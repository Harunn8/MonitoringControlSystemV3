using Application.Responses;
using DeviceApplication.Models;
using DeviceApplication.Repositories.Base;
using DeviceApplication.Responses;
using DeviceApplication.Services.Base;
using McsCore.Entities;
using McsMqtt.Producer;
using McsUserLogs.Models;
using McsUserLogs.Services.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceApplication.Services
{
    public class PagDeviceService : IPagDeviceService
    {
        private readonly IPagDeviceRepository _repository;
        private readonly MqttProducer _mqtt;
        private readonly IUserLogService _userLogService;

        public PagDeviceService(IPagDeviceRepository repository, MqttProducer mqtt, IUserLogService userLogService)
        {
            _repository = repository;
            _mqtt = mqtt;
            _userLogService = userLogService;
        }

        public async Task<bool> AddPagDevice(PagDeviceAddModel pagDeviceModel)
        {
            var response = await _repository.AddPagDevice(pagDeviceModel);

            if (response)
            {
                var userLogModel = new UserLogs()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.PagDeviceService",
                    MethodName = nameof(AddPagDevice),
                    Message = "Pag Device Added",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return response;
            }
            return response;
        }

        public async Task<bool> DeletePagDevice(Guid id)
        {
            var response = _repository.DeletePagDevice(id);

            if (response.IsCompletedSuccessfully)
            {
                var userLogModel = new UserLogs()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.PagDeviceService",
                    MethodName = nameof(AddPagDevice),
                    Message = "Pag Device Added",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<List<PagDeviceResponses>> GetAllPagDevice()
        {
            var response = await _repository.GetAllPagDevice();
            return response;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByCommunicationType(CommunicationType communicationType)
        {
            var response = await _repository.GetPagDeviceByCommunicationType(communicationType);
            return response;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByDeviceId(Guid deviceId)
        {
            var response = await _repository.GetPagDeviceByDeviceId(deviceId);
            return response;
        }

        public async Task<PagDeviceResponses> GetPagDeviceById(Guid id)
        {
            var response = await _repository.GetPagDeviceById(id);
            return response;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByPagId(Guid pagId)
        {
            var response = await _repository.GetPagDeviceByPagId(pagId);
            return response;
        }

        public async Task<bool> UpdatePagDevice(Guid id, PagDeviceAddModel updatePagDeviceModel)
        {
            var response = await _repository.UpdatePagDevice(id, updatePagDeviceModel);
            return response;
        }

        public async Task<bool> StartOrStopCommunication(Guid pagDeviceId, bool isActive)
        {
            var pagDevice = await _repository.GetPagDeviceById(pagDeviceId);
            pagDevice.Id = pagDeviceId;
            pagDevice.IsActive = isActive;

            if (pagDevice != null)
            {
                var payload = JsonConvert.SerializeObject(pagDevice);

                _mqtt.PublishMessage("DCS/StartOrStopCommunication", $"{payload}");

                var userLogModel = new UserLogs()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.PagDeviceService",
                    MethodName = nameof(StartOrStopCommunication),
                    Message = $"{pagDevice.PagDeviceName} communication status updated",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Updated
                };

                await _userLogService.SetEventUserLog(userLogModel);

                return true;
            }
            return false;
        }
    }
}
