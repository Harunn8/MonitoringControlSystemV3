using Application.Models;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using RuleApplication.Responses;
using RuleApplication.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleApplication.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly McsAppDbContext _dbContext;

        public AlarmService(McsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAlarm(AlarmModel alarm)
        {
            await _dbContext.Alarms.AddAsync(alarm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAlarm(Guid id)
        {
            var entity = _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<AlarmResponse> GetAlarmByDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmResponse> GetAlarmByDeviceId(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmResponse> GetAlarmById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmResponse> GetAlarmByParameterId(Guid parameterId)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmResponse> GetAlarmByStatus(Severity status)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmResponse> GetAllAlarm()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAlarm(AlarmModel alarm)
        {
            throw new NotImplementedException();
        }
    }
}
