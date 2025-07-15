using Application.Models;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Base
{
    public interface ITcpDeviceService
    {
        Task<List<TcpDeviceResponses>> GetAllTcpDevice();
        Task<TcpDeviceResponses> GetTcpDeviceById(Guid id);
        Task<TcpDeviceResponses> GetTcpDeviceByIp(string ipAddress);
        Task<TcpDeviceResponses> GetTcpDeviceByName(string name);
        Task<TcpDeviceResponses> GetTcpDeviceByPort(int port);
        Task<TcpDeviceResponses> GetTcpDeviceByIpAndPort(string ipAddress, int port);
        Task AddTcpDevice(TcpDeviceModel tcpDeviceModel);
        Task UpdateTcpDevice(Guid id, TcpDeviceModel tcpDeviceModel);
        Task DeleteTcpDevice(Guid id);
    }
}
