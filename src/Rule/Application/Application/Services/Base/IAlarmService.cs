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
        Task<AlarmResponse> GetAllAlarm();
        Task<AlarmResponse> GetAlarmById(Guid id);
        Task<AlarmResponse> GetAlarmByDateRange(DateTime startDate, DateTime endDate);
        Task<AlarmResponse> GetAlarmByStatus(Severity status);
        Task<AlarmResponse> GetAlarmByDeviceId(Guid deviceId);
        Task<AlarmResponse> GetAlarmByParameterId(Guid parameterId);
        Task AddAlarm(AlarmModel alarm);
        Task UpdateAlarm(AlarmModel alarm);
        Task DeleteAlarm(Guid id);

    }
}
