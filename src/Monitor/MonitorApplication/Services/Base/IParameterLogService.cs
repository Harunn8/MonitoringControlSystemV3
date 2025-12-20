using McsCore.Entities;
using McsCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Services.Base
{
    public interface IParameterLogService
    {
        Task<TableResponse> GetParameterLogsByPage(TableModel tableModel);
        Task<TableResponse> GetParameterLogsOfLastHourByPage(TableModel tableModel);
        Task<TableResponse> GetParameterLogsOfLastDayByPage(TableModel tableModel);
        Task<TableResponse> GetParameterLogsOfLastWeekByPage(TableModel tableModel);
        public void AddParameterLogs(ParameterLogsAdd addModel);
        public bool StartOrStopParameterLogs(Guid parameterSetsId, bool isActive);
        public bool UpdateParameterLog(Guid parameterSetsId, ParameterLogsAdd updatedParameterLogsModel);
    }
}
