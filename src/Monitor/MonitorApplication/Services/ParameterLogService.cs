using AutoMapper;
using McsCore.Entities;
using McsCore.Responses;
using MonitorApplication.Repository.Base;
using MonitorApplication.Responses;
using MonitorApplication.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitorApplication.Services
{
    public class ParameterLogService : IParameterLogService
    {
        private readonly IParameterLogRepository _repository;
        private readonly IMapper _mapper;

        public ParameterLogService(IParameterLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TableResponse> GetParameterLogsByPage(TableModel tableModel)
        {
            return await _repository.GetParameterLogsByPage(tableModel);
        }

        public async Task<TableResponse> GetParameterLogsOfLastDayByPage(TableModel tableModel)
        {
            var entities = await _repository.GetParameterLogsByPage(tableModel);

            var totalDataCount = entities.TotalDataCount;
            var pageSize = entities.PageSize;
            var data = _mapper.Map<List<ParameterLogTableResponse>>(entities.Data);

            TableResponse response = new TableResponse
            {
                Sort = tableModel.Sort,
                SortType = tableModel.SortType,
                Search = tableModel.Search,
                SpeacialSearchField = tableModel.SpeacialSearchField,
                PageSize = pageSize,
                RowSize = tableModel.RowSize,
                ActivePage = tableModel.ActivePage,
                TotalDataCount = totalDataCount,
                Data = data
            };

            return response;
        }

        public Task<TableResponse> GetParameterLogsOfLastHourByPage(TableModel tableModel)
        {
            throw new NotImplementedException();
        }

        public Task<TableResponse> GetParameterLogsOfLastWeekByPage(TableModel tableModel)
        {
            throw new NotImplementedException();
        }

        public void AddParameterLogs(ParameterLogsAdd addModel)
        {
            throw new NotImplementedException();
        }

        public bool StartOrStopParameterLogs(Guid parameterSetsId, bool isActive)
        {
            throw new NotImplementedException();
        }

        public bool UpdateParameterLog(Guid parameterSetsId, ParameterLogsAdd updatedParameterLogsModel)
        {
            throw new NotImplementedException();
        }
    }
}
