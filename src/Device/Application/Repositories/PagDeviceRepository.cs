using AutoMapper;
using DeviceApplication.Models;
using DeviceApplication.Repositories.Base;
using DeviceApplication.Responses;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceApplication.Repositories
{
    public class PagDeviceRepository : IPagDeviceRepository
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PagDeviceRepository(McsAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddPagDevice(PagDeviceAddModel pagDeviceModel)
        {
            try
            {
                pagDeviceModel.IsActive = false;

                var entity = _mapper.Map<PagDevices>(pagDeviceModel);

                await _dbContext.PagDevices.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR : Could not to be add Pag Device ---> ",ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePagDevice(Guid id)
        {
            var pagDevice = await GetPagDeviceById(id);

            if (pagDevice == null) return false;

            try
            {
                var response = _mapper.Map<PagDevices>(pagDevice);
                
                _dbContext.PagDevices.Remove(response);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR : Could not delete to be Pag Device ----> ",ex.Message);
                return false;
            }
        }

        public async Task<List<PagDeviceResponses>> GetAllPagDevice()
        {
            var pagDevices = await _dbContext.PagDevices
                .Include(x => x.Device)
                .ThenInclude(d => d.Alarms)
                .ToListAsync();
            
            var response = _mapper.Map<List<PagDeviceResponses>>(pagDevices);
            return response;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByCommunicationType(CommunicationType communicationType)
        {
            var pagDevices = await _dbContext.PagDevices.Where(x => x.Device.CommunicationType == communicationType)
                .Include(d => d.Device)
                .ThenInclude(d => d.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<PagDeviceResponses>>(pagDevices);
            return response;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByDeviceId(Guid deviceId)
        {
            var pagDevice = await _dbContext.PagDevices.Where(x => x.DeviceId == deviceId)
                .Include(x => x.Device)
                .ThenInclude(d => d.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<PagDeviceResponses>>(pagDevice);
            return response;
        }

        public async Task<PagDevices> GetPagDeviceById(Guid id)
        {
            var pagDevice = await _dbContext.PagDevices.Where(x => x.Id == id)
                .Include(x => x.Device)
                .ThenInclude(d => d.Alarms)
                .FirstOrDefaultAsync();
            
            return pagDevice;
        }

        public async Task<List<PagDeviceResponses>> GetPagDeviceByPagId(Guid pagId)
        {
            var pagDevices = await _dbContext.PagDevices
                .Include(x => x.Device)
                .ThenInclude(d => d.Alarms)
                .Where(x => x.PagId == pagId).ToListAsync();
            
            var response = _mapper.Map<List<PagDeviceResponses>>(pagDevices);
            return response;
        }

        public async Task<bool> UpdatePagDevice(Guid id, PagDevices updatePagDeviceModel)
        {
            var pagDevice = await GetPagDeviceById(id);

            if (pagDevice != null)
            {
                try
                {
                    _dbContext.PagDevices.Update(updatePagDeviceModel);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("ERROR : Could not be update Pag Device -----> ",ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}
