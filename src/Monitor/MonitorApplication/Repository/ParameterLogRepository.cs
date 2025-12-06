using AutoMapper;
using McsCore.Entities;
using McsCore.Responses;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MongoDB.Driver;
using MonitorApplication.Data.Interfaces;
using MonitorApplication.Mapper;
using MonitorApplication.Repository.Base;
using MonitorApplication.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Repository
{
    public class ParameterLogRepository : IParameterLogRepository
    {
        private readonly IParameterLogContext _context;
        private readonly IMapper _mapper;

        public ParameterLogRepository(IParameterLogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private FilterDefinition<ParameterLogs> SetDefaultFilter(TableModel tableModel, FilterDefinitionBuilder<ParameterLogs> builder)
        {
            var filter = builder.Empty;

            if(tableModel.SpeacialSearchField != null)
            {
                ParameterLogSpeacialSearch searchField = new ParameterLogSpeacialSearch();
                searchField = JsonConvert.DeserializeObject<ParameterLogSpeacialSearch>(tableModel.SpeacialSearchField);

                if(searchField.DevicecId != null && searchField.DevicecId != Guid.Empty)
                {
                    var deviceIdFilter = builder.Eq(x => x.DeviceId, searchField.DevicecId);
                    filter &= deviceIdFilter;
                }
                if (searchField.ParameterId != null && searchField.ParameterId != Guid.Empty)
                {
                    var parameterIdFilter = builder.Eq(x => x.ParameterId, searchField.ParameterId);
                    filter &= parameterIdFilter;
                }
                if(searchField.ParameterTimeStamp != null && searchField.ParameterTimeStamp != DateTime.MinValue)
                {
                    var parameterTimeStampFilter = builder.Eq(x => x.ParameterTimeStamp, searchField.ParameterTimeStamp);
                    filter &= parameterTimeStampFilter;
                }
               else
                {
                    if(searchField.StartDateFilter != null || searchField.EndDateFilter != null)
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
            else if(tableModel.Sort != null && tableModel.SortType == "DESC")
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

            var countDocuments = (int) await _context.ParameterLogs
                .Find(filter)
                .Limit(100000000).CountDocumentsAsync();

            var pageSize = (int) Math.Ceiling((double)countDocuments / tableModel.RowSize);
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
    }
}
