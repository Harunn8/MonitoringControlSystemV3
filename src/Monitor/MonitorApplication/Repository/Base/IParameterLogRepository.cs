using McsCore.Entities;
using McsCore.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonitorApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Repository.Base
{
    public interface IParameterLogRepository
    {
        Task<TableResponse> GetParameterLogsByPage(TableModel tableModel);
        Task<List<ParameterLogs>> GetParameterLogsOfLastHourByPage(TableModel tableModel);
        Task<List<ParameterLogs>> GetParameterLogsOfLastDayByPage(TableModel tableModel);
        Task<List<ParameterLogs>> GetParameterLogsOfLastWeekByPage(TableModel tableModel);
        Task<List<ParameterLogs>> GetParameterLogsOfLastMonthByPage(TableModel tableModel);
        public void AddParameterLogs(ParameterLogAddModel addModel);
        Task<bool> StartOrStopParameterLogs(Guid parameterSetsId, bool isActive);
        public bool UpdateParameterLog(Guid parameterSetsId, ParameterLogsAdd updatedParameterLogsModel);
    }
}
