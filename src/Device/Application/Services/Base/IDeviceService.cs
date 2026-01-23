using DeviceApplication.Models;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Services.Base
{
    public interface IDeviceService
    {
        public Task<List<BaseDeviceModel>> GetAllDevice();
        public Task<BaseDeviceModel> GetDeviceById(Guid id);
        public Task<List<BaseDeviceModel>> GetDeviceByBrandName(string brandName);
        public Task<List<BaseDeviceModel>> GetDeviceByModelName(string modelName);
        public Task<List<BaseDeviceModel>> GetDeviceByCategoryName(string categoryName);
        public Task<bool> DeleteDevice(Guid id);
        public Task<List<SnmpDeviceModel>> GetAllSnmpDevice();
        public Task<SnmpDeviceModel> GetSnmpDeviceById(Guid id);
        public Task<List<SnmpDeviceModel>> GetSnmpDeviceByBrandName(string brandName);
        public Task<bool> AddSnmpDevice(SnmpDeviceAddModel snmpDeviceModel);
        public Task<bool> UpdateSnmpDevice(Guid id, SnmpDeviceAddModel updateModel);
        public Task<List<TcpDeviceModel>> GetAllTcpDevice();
        public Task<TcpDeviceModel> GetTcpDeviceById(Guid id);
        public Task<List<TcpDeviceModel>> GetTcpDeviceByBrandName(string brandName);
        public Task<bool> AddTcpDevice(TcpDeviceAddModel tcpDeviceModel);
        public Task<bool> UpdateTcpDevice(Guid id, TcpDeviceAddModel updateModel);
    }
}
