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
    public class TcpDeviceService : ITcpDeviceService
    {
        private readonly MqttProducer _mqtt;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenInformationService _tokenInformationService;
        private readonly IUserLogService _userLogService;

        public TcpDeviceService(MqttProducer mqtt, McsAppDbContext dbContext, IMapper mapper, ITokenInformationService tokenInformationService, IUserLogService userLogService)
        {
            _mqtt = mqtt;
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenInformationService = tokenInformationService;
            _userLogService = userLogService;
        }

        public async Task<List<TcpDeviceResponses>> GetAllTcpDevice()
        {
            var entites = await _dbContext.TcpDevices.ToListAsync();
            var response = _mapper.Map<List<TcpDeviceResponses>>(entites);
            return response;
        }

        public async Task<TcpDeviceResponses> GetTcpDeviceById(Guid id)
        {
           var entity = await _dbContext.TcpDevices.Where(x => x.Id == id).FirstOrDefaultAsync();
           var response = _mapper.Map<TcpDeviceResponses>(entity);
           return response;
        }

        public async Task<TcpDeviceResponses> GetTcpDeviceByIp(string ipAddress)
        {
           var entity = await _dbContext.TcpDevices.Where(x => x.IpAddress == ipAddress).FirstOrDefaultAsync();
           var response = _mapper.Map<TcpDeviceResponses>(entity);
           return response;
        }

        public async Task<TcpDeviceResponses> GetTcpDeviceByIpAndPort(string ipAddress, int port)
        {
           var entity = await _dbContext.TcpDevices
                .Where(x => x.IpAddress == ipAddress && x.Port == port)
                .FirstOrDefaultAsync();
           var response = _mapper.Map<TcpDeviceResponses>(entity);
              return response;
        }

        public async Task<TcpDeviceResponses> GetTcpDeviceByName(string name)
        {
            var entity = await _dbContext.TcpDevices
                .Where(x => x.DeviceName == name)
                .FirstOrDefaultAsync();
            var response = _mapper.Map<TcpDeviceResponses>(entity);
            return response;
        }

        public async Task<TcpDeviceResponses> GetTcpDeviceByPort(int port)
        {
            var entity = await _dbContext.TcpDevices
                .Where(x => x.Port == port)
                .FirstOrDefaultAsync();
            var response = _mapper.Map<TcpDeviceResponses>(entity);
            return response;
        }

        public async Task<TcpDeviceResponses> AddTcpDevice(TcpDeviceModel tcpDeviceModel)
        {
            await _dbContext.TcpDevices.AddAsync(tcpDeviceModel);
            await _dbContext.SaveChangesAsync();

            #region UserLog
            await _userLogService.SetEventUserLog(new UserLogs
            {
                UserName = _tokenInformationService.GetUserName ?? "MCSAdmin",
                AppName = "Snmp Device Service",
                Message = $"Added new SNMP device: {tcpDeviceModel.DeviceName}",
                LogDate = DateTime.UtcNow,
                LogType = UserLogType.Added
            });
            #endregion

            var response =_mapper.Map<TcpDeviceResponses>(tcpDeviceModel);
            return response;

        }

        public Task DeleteTcpDevice(Guid id)
        {
           var device = _dbContext.TcpDevices.Where(x => x.Id == id).FirstOrDefault();
            if (device != null)
              {
                 _dbContext.TcpDevices.Remove(device);
                 _dbContext.SaveChangesAsync();
              }
            return null;
        }

        public async Task UpdateTcpDevice(Guid id, TcpDeviceModel tcpDeviceModel)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.Id == id).FirstOrDefaultAsync();
            _dbContext.Update(tcpDeviceModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartTcpDeviceCommunication(Guid id)
        {
            _mqtt.PublishMessage($"tcp/start/{id}", "Start TCP Communication");
        }
    }
}