using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using McsCore.Responses;
using McsUserLogs.Services.Base;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MonitorApplication.Data.Interfaces;
using MonitorApplication.Models;
using MonitorApplication.Repository.Base;
using MonitorApplication.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApplication.Repository
{
    public class ParameterLogRepository : IParameterLogRepository
    {
        private readonly IParameterLogContext _context;
        private readonly McsAppDbContext _postgreContext;
        private readonly IMapper _mapper;
        private readonly IUserLogService _userLogService;

        public ParameterLogRepository(IParameterLogContext context, McsAppDbContext postgreContext, IMapper mapper, IUserLogService userLogService)
        {
            _context = context;
            _postgreContext = postgreContext;
            _mapper = mapper;
            _userLogService = userLogService;
        }

        private FilterDefinition<ParameterLogs> SetDefaultFilter(TableModel tableModel, FilterDefinitionBuilder<ParameterLogs> builder)
        {
            var filter = builder.Empty;

            if (tableModel.SpeacialSearchField != null)
            {
                ParameterLogSpeacialSearch searchField = new ParameterLogSpeacialSearch();
                searchField = JsonConvert.DeserializeObject<ParameterLogSpeacialSearch>(tableModel.SpeacialSearchField);

                if (searchField.DevicecId != null && searchField.DevicecId != Guid.Empty)
                {
                    var deviceIdFilter = builder.Eq(x => x.DeviceId, searchField.DevicecId);
                    filter &= deviceIdFilter;
                }
                if (searchField.ParameterId != null && searchField.ParameterId != Guid.Empty)
                {
                    var parameterIdFilter = builder.Eq(x => x.ParameterId, searchField.ParameterId);
                    filter &= parameterIdFilter;
                }
                if (searchField.ParameterTimeStamp != null && searchField.ParameterTimeStamp != DateTime.MinValue)
                {
                    var parameterTimeStampFilter = builder.Eq(x => x.ParameterTimeStamp, searchField.ParameterTimeStamp);
                    filter &= parameterTimeStampFilter;
                }
                else
                {
                    if (searchField.StartDateFilter != null || searchField.EndDateFilter != null)
                    {
                        var dateFilter = builder.Gte(x => x.ParameterTimeStamp, searchField.StartDateFilter ?? DateTime.MinValue) &
                                         builder.Lte(x => x.ParameterTimeStamp, searchField.EndDateFilter ?? DateTime.MaxValue);
                        filter &= dateFilter;
                    }
                }
            }

            return filter;
        }

        private SortDefinition<ParameterLogs> SetSortDefiniton(TableModel tableModel)
        {
            SortDefinition<ParameterLogs> sort = null;

            if (tableModel.Sort != null && tableModel.SortType == "ASC")
            {
                sort = Builders<ParameterLogs>.Sort.Ascending(x => x.ParameterTimeStamp);
            }
            else if (tableModel.Sort != null && tableModel.SortType == "DESC")
            {
                sort = Builders<ParameterLogs>.Sort.Descending(x => x.ParameterTimeStamp);
            }
            else
            {
                sort = Builders<ParameterLogs>.Sort.Descending(x => x.ParameterTimeStamp);
            }
            return sort;
        }

        public async Task<TableResponse> GetParameterLogsByPage(TableModel tableModel)
        {
            var builder = Builders<ParameterLogs>.Filter;
            SortDefinition<ParameterLogs> sort = SetSortDefiniton(tableModel);
            FilterDefinition<ParameterLogs> filter = SetDefaultFilter(tableModel, builder);

            var countDocuments = (int)await _context.ParameterLogs
                .Find(filter)
                .Limit(100000000).CountDocumentsAsync();

            var pageSize = (int)Math.Ceiling((double)countDocuments / tableModel.RowSize);
            var activePage = tableModel.ActivePage > pageSize ? pageSize : tableModel.ActivePage;

            if (activePage < 1) activePage = 1;

            var entites = _context.ParameterLogs
                .Find(filter)
                .Sort(sort)
                .Skip(tableModel.RowSize * (activePage - 1))
                .Limit(tableModel.RowSize)
                .ToList();

            var data = _mapper.Map<ParameterLogTableResponse>(entites);

            TableResponse response = new TableResponse
            {
                SpeacialSearchField = tableModel.SpeacialSearchField,
                ActivePage = activePage,
                PageSize = pageSize,
                RowSize = tableModel.RowSize,
                TotalDataCount = countDocuments,
                Search = tableModel.Search,
                Sort = tableModel.Sort,
                SortType = tableModel.SortType,
                Data = data
            };

            return response;

        }

        public async Task<List<ParameterLogs>> GetParameterLogsOfLastDayByPage(TableModel tableModel)
        {
            var builder = Builders<ParameterLogs>.Filter;
            SortDefinition<ParameterLogs> sort = SetSortDefiniton(tableModel);
            FilterDefinition<ParameterLogs> filter = SetDefaultFilter(tableModel, builder);

            DateTime queryTime = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffff"), "yyyy-MM-ddTHH:mm:ss.ffff", CultureInfo.InvariantCulture);
            var lastHourFilter = builder.Where(x => x.ParameterTimeStamp > DateTime.Now.AddHours(-1) && x.ParameterTimeStamp < queryTime);
            filter &= lastHourFilter;

            return await _context.ParameterLogs
                .Find(filter)
                .Sort(sort)
                .Skip(tableModel.RowSize * (tableModel.ActivePage - 1))
                .Limit(tableModel.RowSize)
                .ToListAsync();
        }

        public Task<List<ParameterLogs>> GetParameterLogsOfLastHourByPage(TableModel tableModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<ParameterLogs>> GetParameterLogsOfLastMonthByPage(TableModel tableModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<ParameterLogs>> GetParameterLogsOfLastWeekByPage(TableModel tableModel)
        {
            throw new NotImplementedException();
        }

        public async void AddParameterLogs(ParameterLogAddModel addModel)
        {
            try
            {
                await _postgreContext.AddAsync(addModel);
                await _postgreContext.SaveChangesAsync();

                var userLog = new UserLogs
                {
                    Id = Guid.NewGuid(),
                    UserName = "McsAdmin",
                    AppName = "MonitorAPI",
                    Message = $"Parameter logs added with Id: {addModel.ParameterSetsName}",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : Could not add parameter logs", ex.Message);
            }
        }

        public async Task<bool> StartOrStopParameterLogs(Guid parameterSetsId, bool isActive)
        {
            // TODO : Bu fonksiyon daha kısa ve kullanışlı yazılabilir. Şimdilik bu şekilde stabil ve çalışır olduğunu gördükten sonra refactor edeceğim.
            var response = false;
            var parameterSets = await GetParameterLogsByParameterSetsId(parameterSetsId);

            if (parameterSets != null)
            {
                if (isActive && parameterSets.isActive == false)
                {
                    var updateParameterSets = new ParameterLogsAdd
                    {
                        Id = parameterSets.Id,
                        ParameterSetsName = parameterSets.ParameterSetsName,
                        ParameterId = parameterSets.ParameterId,
                        DeviceId = parameterSets.DeviceId,
                        isActive = isActive
                    };

                    response = UpdateParameterLog(parameterSetsId, updateParameterSets);
                }
                else if (!isActive && parameterSets.isActive == true)
                {
                    var updateParameterSets = new ParameterLogsAdd
                    {
                        Id = parameterSets.Id,
                        ParameterSetsName = parameterSets.ParameterSetsName,
                        ParameterId = parameterSets.ParameterId,
                        DeviceId = parameterSets.DeviceId,
                        isActive = isActive
                    };

                    response = UpdateParameterLog(parameterSetsId, updateParameterSets);
                }

                var userLog = new UserLogs
                {
                    Id = Guid.NewGuid(),
                    UserName = "McsAdmin",
                    AppName = "MonitorAPI",
                    Message = $"Parameter logs updated",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Updated
                };

                await _userLogService.SetEventUserLog(userLog);
            }

            return response;
        }

        private async Task<ParameterLogsAdd> GetParameterLogsByParameterSetsId(Guid parameterSetsId)
        {
            var response = await _postgreContext.ParameterLogs.Where(x => x.Id == parameterSetsId).FirstOrDefaultAsync();
            return response;
        }

        public bool UpdateParameterLog(Guid parameterSetsId, ParameterLogsAdd updatedParameterLogsModel)
        {
            var sets = GetParameterLogsByParameterSetsId(parameterSetsId);

            if (sets != null)
            {
                try
                {
                    _postgreContext.ParameterLogs.Update(updatedParameterLogsModel);
                    _postgreContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : Could not update parameter sets model", ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}