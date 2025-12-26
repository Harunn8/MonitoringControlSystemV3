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
using Newtonsoft.Json;
using DeviceApplication.Models;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Application.Services
{
    public class SnmpDeviceService : ISnmpDeviceService
    {
        private readonly MqttProducer _mqtt;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserLogService _userLog;
        private readonly ITokenInformationService _tokenInformation;
        

        public SnmpDeviceService(MqttProducer mqtt, McsAppDbContext dbContext, IMapper mapper, IUserLogService userLog, ITokenInformationService tokenInformation)
        {
            _mqtt = mqtt;
            _dbContext = dbContext;
            _mapper = mapper;
            _userLog = userLog;
            _tokenInformation = tokenInformation;
        }

        public async Task<List<SnmpDeviceResponses>> GetAllSnmpDevice()
        {
            var entities = await _dbContext.SnmpDevices.ToListAsync();
            var responses = _mapper.Map<List<SnmpDeviceResponses>>(entities);
            return responses;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceById(Guid id)
        {
            var entity = await _dbContext.SnmpDevices.Include(x => x.Parameters).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null) return null;
            var response = _mapper.Map<SnmpDeviceResponses>(entity);
            return response;
        }

        public async Task<SnmpDeviceResponses> GetSnmpDeviceByIp(string ipAddress)
        {
            var entity = await _dbContext.SnmpDevices.Include(x => x.Parameters).Where(x => x.IpAddress == ipAddress).FirstOrDefaultAsync();
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

        public async Task<SnmpDeviceResponses> AddSnmpDevice(SnmpDevice snmpDeviceModel)
        {
            var snmpDeviceData = new SnmpDevice
            {
                Id = Guid.NewGuid(),
                DeviceName = snmpDeviceModel.DeviceName,
                IpAddress = snmpDeviceModel.IpAddress,
                Port = snmpDeviceModel.Port,
                SnmpVersion = snmpDeviceModel.SnmpVersion,
                Timeout = snmpDeviceModel.Timeout,
                Retry = snmpDeviceModel.Retry,
                Version = snmpDeviceModel.Version,
                ReadCommunity = snmpDeviceModel.ReadCommunity,
                WriteCommunity = snmpDeviceModel.WriteCommunity
            };

            try
            {
                await _dbContext.AddAsync(snmpDeviceModel);

                _dbContext.SaveChanges();

                _dbContext.Entry(snmpDeviceModel).State = EntityState.Detached;

                #region UserLog
                await _userLog.SetEventUserLog(new UserLogs
                {
                    UserName = _tokenInformation.GetUserName ?? "MCSAdmin",
                    AppName = "Snmp Device Service",
                    Message = $"Added new SNMP device: {snmpDeviceModel.DeviceName}",
                    LogDate = DateTime.UtcNow,
                    LogType = UserLogType.Added
                });
                #endregion

                var response = _mapper.Map<SnmpDeviceResponses>(snmpDeviceModel);
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task DeleteSnmpDevice(Guid id)
        {
            var device = await GetSnmpDeviceById(id);
            
            _dbContext.Remove(id);
            
            await _dbContext.SaveChangesAsync();

            #region UserLog
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserName = _tokenInformation.GetUserName ?? "MCSAdmin",
                AppName = "Snmp Device Service",
                Message= $"Deleted SNMP device : {device.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Deleted
            });
            #endregion
        }

        public async Task UpdateSnmpDevice(Guid id, SnmpDevice snmpDeviceModel)
        {
            var entity = await GetSnmpDeviceById(id);
         
            _dbContext.Update(snmpDeviceModel);
            
            await _dbContext.SaveChangesAsync();

            #region UserLog
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserName = _tokenInformation.GetUserName ?? "MCSAdmin",
                AppName = "Snmp Device Service",
                Message = $"Updated SNMP device for : {snmpDeviceModel.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Updated
            });
            #endregion
        }

        public async Task StartSnmpCommunication(Guid id)
        {
            var device = await GetSnmpDeviceById(id);

            var payload = JsonConvert.SerializeObject(device);

            _mqtt.PublishMessage("DCS/SNMP/Start",payload);

            #region UserLog
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserName = _tokenInformation.GetUserName ?? "MCSAdmin",
                AppName = "Snmp Device Service",
                Message = $"Started SNMP communication for : {device.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Updated
            });
            #endregion
        }

        public async Task StopSnmpCommunication(Guid id)
        {
            var device = await GetSnmpDeviceById(id);
            
            _mqtt.PublishMessage("SNMP/Stop",$"{ device}");

            #region UserLog
            await _userLog.SetEventUserLog(new UserLogs
            {
                UserName = _tokenInformation.GetUserName ?? "MCSAdmin",
                AppName = "Snmp Device Service",
                Message = $"Stopped SNMP communication for {device.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Updated
            });
            #endregion
        }

        public Task<SnmpCommandModel> SendSnmpCommand(SnmpCommandModel snmpCommandModel)
        {
            try
            {
                var payload = JsonConvert.SerializeObject(snmpCommandModel);

                _mqtt.PublishMessage("DCS/SNMP/SendCommand", payload);

                var commandModel = new SnmpCommandModel
                {
                    SnmpDevice = snmpCommandModel.SnmpDevice,
                    Oid = snmpCommandModel.Oid,
                    Value = snmpCommandModel.Value

                };

                _mqtt.PublishMessage("telemetry", "Command was send successful");

                return Task.FromResult(commandModel);
            }

            catch (Exception ex)
            {
                throw new NullReferenceException("Command must not be empty");
            }

            
        }
    }
}
