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
        Task<List<Alarms>> GetAlarmById(Guid id);
        //Task<List<AlarmResponse>> GetAlarmByDateRange(DateTime startDate, DateTime endDate);
        Task<List<Alarms>> GetAlarmByStatus(Severity status);
        Task<List<Alarms>> GetAlarmByDeviceId(Guid deviceId);
        Task<List<Alarms>> GetAlarmByParameterId(Guid parameterId);
        Task AddAlarm(AlarmModel alarm);
        Task UpdateAlarm(Guid id,AlarmModel alarm);
        Task DeleteAlarm(Guid id);

    }
}
