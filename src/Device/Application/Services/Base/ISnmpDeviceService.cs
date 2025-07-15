using Application.Models;
using Application.Responses;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Base
{
    public interface ISnmpDeviceService
    {
        Task<List<SnmpDeviceResponses>> GetAllSnmpDevice();
        Task<SnmpDeviceResponses> GetSnmpDeviceById(Guid id);
        Task<SnmpDeviceResponses> GetSnmpDeviceByIp(string ipAddress);
        Task<SnmpDeviceResponses> GetSnmpDeviceByName(string name);
        Task<SnmpDeviceResponses> GetSnmpDeviceByPort(int port);
        Task<SnmpDeviceResponses> GetSnmpDeviceByIpAndPort(string ipAddress, int port);
        Task AddSnmpDevice(SnmpDevice snmpDeviceModel);
        Task UpdateSnmpDevice(Guid id, SnmpDevice snmpDeviceModel);
        Task DeleteSnmpDevice(Guid id);
        Task StartSnmpCommunication(Guid id);
        Task StopSnmpCommunication(Guid id);
    }
}
