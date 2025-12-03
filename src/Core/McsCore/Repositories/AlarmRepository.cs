using McsCore.AppDbContext;
using McsCore.AppDbContext.Mongo;
using McsCore.Entities;
using McsCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Repositories
{
    public class AlarmRepository : IAlarmRepository
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMongoCollection<ActiveAlarms> _activeAlarm;
        private readonly IMongoCollection<HistoricalAlarms> _historicalAlarm;

        public AlarmRepository(McsAppDbContext dbContext, MongoDbContext mongoContext, IMongoDatabase activeAlarm, IMongoDatabase historicalAlarm)
        {
            _dbContext = dbContext;
            _activeAlarm = mongoContext.ActiveAlarms;
            _historicalAlarm = mongoContext.HistoricalAlarms;
            _activeAlarm = activeAlarm.GetCollection<ActiveAlarms>("ActiveAlarms");
            _historicalAlarm = historicalAlarm.GetCollection<HistoricalAlarms>("HistoricalAlarms");
        }

        public async Task<Alarms> AddAlarm(Alarms alarm)
        {
            try
            {
                await _dbContext.Alarms.AddAsync(alarm);
                await _dbContext.SaveChangesAsync();
                return alarm;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR ADDING ALARM: " + ex.Message);
                return null;
            }

        }

        public async Task<bool> DeleteAlarm(Guid id)
        {
            var alarm = _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
            if (alarm != null)
            {
                try
                {
                    _dbContext.Remove(alarm);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR DELETING ALARM: " + ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<List<ActiveAlarms>> GetActiveAlarm()
        {
            var activeAlarms = await _activeAlarm.Find(_ => true).ToListAsync();
            return activeAlarms;
        }

        public async Task<List<Alarms>> GetAlarmByDeviceId(Guid deviceId)
        {
            var alarms = await _dbContext.Alarms
                 .Where(k => k.DeviceId == deviceId)
                 .ToListAsync();

            return alarms;
        }

        public async Task<List<Alarms>> GetAlarmById(Guid id)
        {
            var alarms = await _dbContext.Alarms
               .Where(k => k.Id == id)
               .ToListAsync();

            return alarms;
        }

        public async Task<List<Alarms>> GetAlarmByParameterId(Guid parameterId)
        {
            var alarms = await _dbContext.Alarms
              .Where(k => k.ParameterId == parameterId)
              .ToListAsync();

            return alarms;
        }

        public async Task<List<Alarms>> GetAlarmByStatus(Severity status)
        {
            var alarms = await _dbContext.Alarms
                .Where(k => k.Severity == status)
                .ToListAsync();

            return alarms;
        }

        public async Task<List<Alarms>> GetAllAlarm()
        {
            var alarms = await _dbContext.Alarms
                .Where(k => k.ParameterId != System.Guid.Empty)
                .ToListAsync();

            return alarms;
        }

        public async Task<Alarms> UpdateAlarm(Guid alarmId, Alarms alarm)
        {
            var existingAlarm = await _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == alarmId);
            if (existingAlarm != null)
            {
                try
                {
                    _dbContext.Update(alarm);
                    await _dbContext.SaveChangesAsync();
                    return alarm;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR UPDATING ALARM: " + ex.Message);
                    return null;
                }
            }
            return null;
        }
    }
}
