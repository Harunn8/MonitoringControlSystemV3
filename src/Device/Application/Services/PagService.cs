using DeviceApplication.Repositories.Base;
using DeviceApplication.Responses;
using DeviceApplication.Services.Base;
using McsCore.Entities;
using McsUserLogs.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceApplication.Services
{
    public class PagService : IPagService
    {
        private readonly IPagRepository _repository;
        private readonly IUserLogService _userLogService;

        public PagService(IPagRepository repository, IUserLogService userLogService)
        {
            _repository = repository;
            _userLogService = userLogService;
        }

        public async Task<bool> AddPag(Pags pagModel)
        {
            var response = await _repository.AddPag(pagModel);

            if (response)
            {
                var userLogModel = new UserLogs()
                {
                    //Id = Guid.NewGuid(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.PagService",
                    MethodName = nameof(AddPag),
                    Message = $"{pagModel.Name} Communication Status Updated",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Added
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePag(Guid id)
        {
            var response = await _repository.DeletePag(id);

            if (response)
            {
                var userLogModel = new UserLogs()
                {
                    //Id = Guid.NewGuid(),
                    UserName = "McsAdmin",
                    AppName = "DeviceApplication.Services.PagService",
                    MethodName = nameof(DeletePag),
                    Message = $"Pag Deleted",
                    LogDate = DateTime.Now,
                    LogType = UserLogType.Deleted
                };

                await _userLogService.SetEventUserLog(userLogModel);
                return true;
            }
            return false;
        }

        public async Task<List<PagResponse>> GetAllPag()
        {
            var response = await _repository.GetAllPag();

            if (response != null) return response;
            return null;
        }

        public async Task<PagResponse> GetPagById(Guid id)
        {
            var response = await _repository.GetPagById(id);

            if (response != null) return response;
            return null;
        }

        public async Task<PagResponse> GetPagByName(string name)
        {
            var response = await _repository.GetPagByName(name);

            if (response != null) return response;
            return null;
        }

        public async Task<bool> UpdatePag(Guid id, Pags pagModel)
        {
            var response = await _repository.UpdatePag(id, pagModel);
            if (response)
            {
                if (response)
                {
                    var userLogModel = new UserLogs()
                    {
                        //Id = Guid.NewGuid(),
                        UserName = "McsAdmin",
                        AppName = "DeviceApplication.Services.PagService",
                        MethodName = nameof(UpdatePag),
                        Message = $"Pag Deleted",
                        LogDate = DateTime.Now,
                        LogType = UserLogType.Deleted
                    };

                    await _userLogService.SetEventUserLog(userLogModel);
                    return true;
                }
            }
            return false;
        }
    }
}
