using DeviceApplication.Models;
using DeviceApplication.Repositories.Base;
using DeviceApplication.Services.Base;
using McsCore.Entities;
using McsMqtt.Producer;
using McsUserLogs.Models;
using McsUserLogs.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserLogService _userLogService;

        public DeviceService(IDeviceRepository deviceRepository, IUserLogService userLogService)
        {
            _deviceRepository = deviceRepository;
            _userLogService = userLogService;
        }

        public async Task<bool> AddSnmpDevice(SnmpDeviceAddModel snmpDeviceModel)
        {
            var response = await _deviceRepository.AddSnmpDevice(snmpDeviceModel);
            if (response)
            {
                var userLogModel = new McsUserLogModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.DeviceService",
                    MethodName = nameof(AddSnmpDevice),
                    Message = "SNMP Device Added",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<bool> AddTcpDevice(TcpDeviceAddModel tcpDeviceModel)
        {
            var response = await _deviceRepository.AddTcpDevice(tcpDeviceModel);
            if (response)
            {
                var userLogModel = new McsUserLogModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.DeviceService",
                    MethodName = nameof(AddTcpDevice),
                    Message = "TCP Device Added",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDevice(Guid id)
        {
            var response = await _deviceRepository.DeleteDevice(id);
            if (response)
            {
                var userLogModel = new McsUserLogModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.DeviceService",
                    MethodName = nameof(DeleteDevice),
                    Message = $"Device Deleted",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Deleted
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<List<BaseDeviceModel>> GetAllDevice()
        {
            var response = await _deviceRepository.GetAllDevice();
            return response;
        }

        public async Task<List<SnmpDeviceModel>> GetAllSnmpDevice()
        {
            var response = await _deviceRepository.GetAllSnmpDevice();
            return response;
        }

        public async Task<List<TcpDeviceModel>> GetAllTcpDevice()
        {
            var response = await _deviceRepository.GetAllTcpDevice();
            return response;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByBrandName(string brandName)
        {
            var response = await _deviceRepository.GetDeviceByBrandName(brandName);
            return response;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByCategoryName(string categoryName)
        {
            var response = await _deviceRepository.GetDeviceByCategoryName(categoryName);
            return response;
        }

        public async Task<BaseDeviceModel> GetDeviceById(Guid id)
        {
            var response = await _deviceRepository.GetDeviceById(id);
            return response;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByModelName(string modelName)
        {
            var response = await _deviceRepository.GetDeviceByModelName(modelName);
            return response;
        }

        public async Task<List<SnmpDeviceModel>> GetSnmpDeviceByBrandName(string brandName)
        {
            var response = await _deviceRepository.GetSnmpDeviceByBrandName(brandName);
            return response;
        }

        public async Task<SnmpDeviceModel> GetSnmpDeviceById(Guid id)
        {
            var response = await _deviceRepository.GetSnmpDeviceById(id);
            return response;
        }

        public async Task<List<TcpDeviceModel>> GetTcpDeviceByBrandName(string brandName)
        {
            var response = await _deviceRepository.GetTcpDeviceByBrandName(brandName);
            return response;
        }

        public async Task<TcpDeviceModel> GetTcpDeviceById(Guid id)
        {
            var response = await _deviceRepository.GetTcpDeviceById(id);
            return response;
        }

        public async Task<bool> UpdateSnmpDevice(Guid id, SnmpDeviceAddModel updateModel)
        {
            var response = await _deviceRepository.UpdateSnmpDevice(id, updateModel);
            if (response)
            {
                var userLogModel = new McsUserLogModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.DeviceService",
                    MethodName = nameof(UpdateSnmpDevice),
                    Message = $"Device Updated",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Updated
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTcpDevice(Guid id, TcpDeviceAddModel updateModel)
        {
            var response = await _deviceRepository.UpdateTcpDevice(id, updateModel);
            if (response)
            {
                if (response)
                {
                    var userLogModel = new McsUserLogModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "McsAdmin",
                        AppName = "DeviceApplication.Services.DeviceService",
                        MethodName = nameof(UpdateTcpDevice),
                        Message = $"Device Updated",
                        LogDate = DateTime.Now,
                        LogType = UserLogType.Updated
                    };
                    await _userLogService.SetEventUserLog(userLogModel);
                    return true;
                }
            }
            return false;
        }
    }
}