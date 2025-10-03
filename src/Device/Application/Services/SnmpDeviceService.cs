using Application.Models;
using Application.Responses;
using Application.Services.Base;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using McsMqtt.Producer;
using McsUserLogs.Services.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenInformation.Base;

namespace Application.Services
{
    public class SnmpDeviceService : ISnmpDeviceService
    {
        private readonly MqttProducer _mqtt;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserLogService _userLog;
        private readonly ITokenInformationService _tokenInfo;

        public SnmpDeviceService(MqttProducer mqtt, McsAppDbContext dbContext, IMapper mapper, IUserLogService userLog, ITokenInformationService tokenInformation)
        {
            _mqtt = mqtt;
            _dbContext = dbContext;
            _mapper = mapper;
            _userLog = userLog;
            _tokenInfo = tokenInformation;
        }

        public async Task<List<SnmpDeviceResponses>> GetAllSnmpDevice()
        {
            var entities = await _dbContext.SnmpDevices.ToListAsync();
            var responses = _mapper.Map<List<SnmpDeviceResponses>>(entities);
            return responses;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceById(Guid id)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null) return null;
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceByIp(string ipAddress)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.IpAddress == ipAddress).FirstOrDefaultAsync();
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceByIpAndPort(string ipAddress, int port)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.IpAddress == ipAddress && x.Port == port).FirstOrDefaultAsync();
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceByName(string name)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.DeviceName == name).FirstOrDefaultAsync();
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceByPort(int port)
        {
            var entity = _dbContext.SnmpDevices.Where(x => x.Port == port).FirstOrDefaultAsync();
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task AddSnmpDevice(SnmpDevice snmpDeviceModel)
        {
            await _dbContext.AddAsync(snmpDeviceModel);
            _dbContext.SaveChanges();
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserId = _tokenInfo.UserId,
                UserName = _tokenInfo.UserName,
                AppName = "Snmp Device Service",
                Message= $"Added new SNMP device: {snmpDeviceModel.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Added
            });
        }

        public async Task DeleteSnmpDevice(Guid id)
        {
            var device = await GetSnmpDeviceById(id);
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserId = _tokenInfo.UserId,
                UserName = _tokenInfo.UserName,
                AppName = "Snmp Device Service",
                Message= $"Deleted SNMP device : {device.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Deleted
            });
        }

        public async Task UpdateSnmpDevice(Guid id, SnmpDevice snmpDeviceModel)
        {
            var entity = await GetSnmpDeviceById(id);
            _dbContext.Update(snmpDeviceModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartSnmpCommunication(Guid id)
        {
            var device = await GetSnmpDeviceById(id);
            _mqtt.PublishMessage("SNMP/Start",$"{device}");
            //await _userLog.SetEventUserLog(new UserLogs
            //{
            //    UserId = _tokenInfo.UserId,
            //    UserName = _tokenInfo.UserName,
            //    AppName = "Snmp Device Service",
            //    Message= $"Started SNMP communication for : {device.DeviceName}",
            //    LogDate = DateTime.UtcNow,
            //    LogType = UserLogType.Updated
            //});
        }

        public async Task StopSnmpCommunication(Guid id)
        {
            var device = await GetSnmpDeviceById(id);
            _mqtt.PublishMessage("SNMP/Stop",$"{ device}");
            await _userLog.SetEventUserLog(new UserLogs
            //{
            //    UserId = _tokenInfo.UserId,
            //    UserName = _tokenInfo.UserName,
            //    AppName = "Snmp Device Service",
            //    Message = $"Stopped SNMP communication for {device.DeviceName}",
            //    LogDate = DateTime.UtcNow,
            //    LogType = UserLogType.Updated
            //});
        }
    }
}
