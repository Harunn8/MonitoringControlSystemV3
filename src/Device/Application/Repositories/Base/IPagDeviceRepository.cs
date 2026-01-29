using DeviceApplication.Models;
using DeviceApplication.Responses;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Repositories.Base
{
    public interface IPagDeviceRepository
    {
        public Task<List<PagDeviceResponses>> GetAllPagDevice();
        public Task<PagDeviceResponses> GetPagDeviceById(Guid id);
        public Task<List<PagDeviceResponses>> GetPagDeviceByDeviceId(Guid deviceId);
        public Task<List<PagDeviceResponses>> GetPagDeviceByPagId(Guid pagId);
        public Task<List<PagDeviceResponses>> GetPagDeviceByCommunicationType(CommunicationType communicationType);
        public Task<bool> AddPagDevice(PagDeviceAddModel pagDeviceModel);
        public Task<bool> UpdatePagDevice(Guid id, PagDeviceAddModel updatePagDeviceModel);
        public Task<bool> DeletePagDevice(Guid id);
    }
}
