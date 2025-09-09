using Application.Models;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using RuleApplication.Responses;
using RuleApplication.Services.Base;
using RuleApplication.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuleApplication.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AlarmValidator _validator;

        public AlarmService(McsAppDbContext dbContext, AlarmValidator validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task AddAlarm(AlarmModel alarm)
        {
            await _dbContext.Alarms.AddAsync(alarm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAlarm(Guid id)
        {
            var entity = _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _dbContext.Alarms.Remove(entity.Result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<AlarmResponse>> GetAlarmByDateRange(DateTime startDate, DateTime endDate)
        {
            var entites = await _dbContext.Alarms.Where(x => x.CreateDate>= startDate && x.CreateDate <= endDate).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAlarmByDeviceId(Guid deviceId)
        {
            var entites = await _dbContext.Alarms.Where(x => x.DeviceId == deviceId).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<AlarmResponse> GetAlarmById(Guid id)
        {
            var entity =  await _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
            var response = _mapper.Map<AlarmResponse>(entity);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAlarmByParameterId(Guid parameterId)
        {
            var entites =  await _dbContext.Alarms.Where(x => x.ParameterId == parameterId).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAlarmByStatus(Severity status)
        {
           var entites =  await _dbContext.Alarms.Where(x => x.Severity == status).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAllAlarm()
        {
            var entites =  await _dbContext.Alarms.ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task UpdateAlarm(Guid id,AlarmModel alarm)
        {
            var existingAlarm = await GetAlarmById(id);
            if (alarm != null)
            {
                _dbContext.Alarms.Update(alarm);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
