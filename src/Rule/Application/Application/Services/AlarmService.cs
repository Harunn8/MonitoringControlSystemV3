using Application.Models;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using McsMqtt.Producer;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;
using RuleApplication.Responses;
using RuleApplication.Services.Base;
using RuleApplication.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Serilog;

namespace RuleApplication.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AlarmValidator _validator;
        private readonly MqttProducer _mqtt;

        public AlarmService(McsAppDbContext dbContext, AlarmValidator validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task AddAlarm(AlarmModel alarm)
        {
            try
            {
                await _dbContext.Alarms.AddAsync(alarm);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Alarm could not added ", ex.Message);
            }
        }

        public async Task DeleteAlarm(Guid id)
        {
            var entity = _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                var payload = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
                _mqtt.PublishMessage("RE/DeleteAlarm", $"{payload}");
                _dbContext.Alarms.Remove(entity.Result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<AlarmResponse>> GetAlarmByDateRange(DateTime startDate, DateTime endDate)
        {
            var entites = await _dbContext.Alarms.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate).ToListAsync();
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
            var entity = await _dbContext.Alarms.FirstOrDefaultAsync(x => x.Id == id);
            var response = _mapper.Map<AlarmResponse>(entity);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAlarmByParameterId(Guid parameterId)
        {
            var entites = await _dbContext.Alarms.Where(x => x.ParameterId == parameterId).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<List<AlarmResponse>> GetAlarmByStatus(Severity status)
        {
            var entites = await _dbContext.Alarms.Where(x => x.Severity == status).ToListAsync();
            var response = _mapper.Map<List<AlarmResponse>>(entites);
            return response;
        }

        public async Task<List<Alarms>> GetAllAlarm()
        {
            var entites = await _dbContext.Alarms.ToListAsync();

            if (entites.Count() == 0) return null;

            return entites;
        }

        public async Task UpdateAlarm(Guid id, AlarmModel alarm)
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
