using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Repositories.Base
{
    public interface IAlarmRepository
    {
        public Task<List<Alarms>> GetAllAlarm();
        public Task<List<Alarms>> GetAlarmById(Guid id);
        public Task<List<Alarms>> GetAlarmByStatus(Severity status);
        public Task<List<ActiveAlarms>> GetActiveAlarm();
        public Task<List<Alarms>> GetAlarmByDeviceId(Guid deviceId);
        public Task<List<Alarms>> GetAlarmByParameterId(Guid parameterId);
        public Task<Alarms> AddAlarm(Alarms alarm);
        public Task<Alarms> UpdateAlarm(Guid alarmId, Alarms alarm);
        public Task<bool> DeleteAlarm(Guid id);
    }
}
