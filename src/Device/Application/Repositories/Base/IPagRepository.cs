using DeviceApplication.Responses;
using McsCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Repositories.Base
{
    public interface IPagRepository
    {
        public Task<List<PagResponse>> GetAllPag();
        public Task<PagResponse> GetPagById(Guid id);
        public Task<PagResponse> GetPagByName(string name);
        public Task<bool> AddPag(Pags pagModel);
        public Task<bool> UpdatePag(Guid id, Pags pagModel);
        public Task<bool> DeletePag(Guid id);
    }
}
