using Application.Models;
using Application.Responses;
using Application.Services.Base;
using AutoMapper;
using McsCore.AppDbContext;
using McsCore.Entities;
using McsMqtt.Producer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SnmpDeviceService : ISnmpDeviceService
    {
        private readonly MqttProducer _mqtt;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public SnmpDeviceService(MqttProducer mqtt, McsAppDbContext dbContext, IMapper mapper)
        {
            _mqtt = mqtt;
            _dbContext = dbContext;
            _mapper = mapper;
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

        }

        public async Task DeleteSnmpDevice(Guid id)
        {
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSnmpDevice(Guid id, SnmpDevice snmpDeviceModel)
        {
            var entity = await _dbContext.SnmpDevices.Where(x => x.Id == id).FirstOrDefaultAsync();
            _dbContext.Update(snmpDeviceModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task StartSnmpCommunication(Guid id)
        {
            _mqtt.PublishMessage($"snmp/start/{id}", "Start SNMP Communication");
        }

        public async Task StopSnmpCommunication(Guid id)
        {
            _mqtt.PublishMessage($"snmp/stop/{id}", "Start SNMP Communication");
        }
    }
}
