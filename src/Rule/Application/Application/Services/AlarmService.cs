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
using McsCore.Repositories;
using System.Reflection.Metadata.Ecma335;
using McsCore.Repositories.Base;

namespace RuleApplication.Services
{
    public class AlarmService : IAlarmService
    {
        private readonly IAlarmRepository _alarmRepository;
        private readonly IMapper _mapper;
        private readonly AlarmValidator _validator;
        private readonly MqttProducer _mqtt;

        public AlarmService(IAlarmRepository alarmRepository, AlarmValidator validator)
        {
            _alarmRepository = alarmRepository;
            _validator = validator;
        }

        public async Task AddAlarm(AlarmModel alarm)
        {
            try
            {
               await _alarmRepository.AddAlarm(alarm);
            }
            catch (Exception ex)
            {
                Log.Error("Alarm could not added ", ex.Message);
            }
        }

        public async Task DeleteAlarm(Guid id)
        {
           await _alarmRepository.DeleteAlarm(id);
        }

        //public async Task<List<AlarmResponse>> GetAlarmByDateRange(DateTime startDate, DateTime endDate)
        //{
        //    var entites = await _dbContext.Alarms.Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate).ToListAsync();
        //    var response = _mapper.Map<List<AlarmResponse>>(entites);
        //    return response;
        //}

        public async Task<List<Alarms>> GetAlarmByDeviceId(Guid deviceId)
        {
            var result = await _alarmRepository.GetAlarmByDeviceId(deviceId);
            return result;
        }

        public async Task<List<Alarms>> GetAlarmById(Guid id)
        {
            var result = await _alarmRepository.GetAlarmById(id);
            return result;
        }

        public async Task<List<Alarms>> GetAlarmByParameterId(Guid parameterId)
        {
            var result = await _alarmRepository.GetAlarmByParameterId(parameterId);
            return result;
        }

        public async Task<List<Alarms>> GetAlarmByStatus(Severity status)
        {
            var result = await _alarmRepository.GetAlarmByStatus(status);
            return result;
        }

        public async Task<List<Alarms>> GetAllAlarm()
        {
            var result = await _alarmRepository.GetAllAlarm();
            return result;
        }

        public async Task UpdateAlarm(Guid id, AlarmModel alarm)
        {
            var result = await _alarmRepository.UpdateAlarm(id,alarm);
        }
    }
}
