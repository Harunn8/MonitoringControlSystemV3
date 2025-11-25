using Application.Models;
using McsCore.Entities;
using RuleApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Services.Base
{
    public interface IAlarmService
    {
        Task<List<Alarms>> GetAllAlarm();
        Task<AlarmResponse> GetAlarmById(Guid id);
        Task<List<AlarmResponse>> GetAlarmByDateRange(DateTime startDate, DateTime endDate);
        Task<List<AlarmResponse>> GetAlarmByStatus(Severity status);
        Task<List<AlarmResponse>> GetAlarmByDeviceId(Guid deviceId);
        Task<List<AlarmResponse>> GetAlarmByParameterId(Guid parameterId);
        Task AddAlarm(AlarmModel alarm);
        Task UpdateAlarm(Guid id,AlarmModel alarm);
        Task DeleteAlarm(Guid id);

    }
}
