using AutoMapper;
using DeviceApplication.Models;
using DeviceApplication.Repositories.Base;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace DeviceApplication.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeviceRepository(McsAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddSnmpDevice(SnmpDeviceAddModel snmpDeviceModel)
        {
            try
            {
                snmpDeviceModel.CommunicationData = JsonConvert.SerializeObject(snmpDeviceModel.Parameters);
                
                await _dbContext.Devices.AddAsync(snmpDeviceModel);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR : Could not be add device ---> ", ex.Message);
                return false;
            }
        }

        public async Task<bool> AddTcpDevice(TcpDeviceAddModel tcpDeviceModel)
        {
            try
            {
                tcpDeviceModel.CommunicationData = JsonConvert.SerializeObject(tcpDeviceModel.TcpCommunicationData);
                
                await _dbContext.Devices.AddAsync(tcpDeviceModel);
                await _dbContext.SaveChangesAsync();
                
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR : Could not be add device ---> ", ex.Message);
                return false;
            }
           
        }

        public async Task<bool> DeleteDevice(Guid id)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id).FirstOrDefaultAsync();
            if(device != null)
            {
                _dbContext.Devices.Remove(device);
                return true;
            }
            return false;
        }

        public async Task<List<BaseDeviceModel>> GetAllDevice()
        {
            var devices = await _dbContext.Devices
                .Include(x => x.Alarms)
                .ToListAsync();

            return devices;
        }

        public async Task<List<SnmpDeviceModel>> GetAllSnmpDevice()
        {
            var devices = await _dbContext.Devices.Where(x => x.CommunicationType == CommunicationType.SNMP)
                .Include(x => x.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<SnmpDeviceModel>>(devices);
            return response;
        }

        public async Task<List<TcpDeviceModel>> GetAllTcpDevice()
        {
            var devices = await _dbContext.Devices.Where(x => x.CommunicationType == CommunicationType.TCP)
                .Include(x => x.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<TcpDeviceModel>>(devices);
            return response;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByBrandName(string brandName)
        {
            var devices = await _dbContext.Devices.Where(x => x.BrandName == brandName)
                .Include(x => x.Alarms)
                .ToListAsync();
            return devices;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByCategoryName(string categoryName)
        {
            var devices = await _dbContext.Devices.Where(x => x.Category == categoryName)
                .Include(x => x.Alarms)
                .ToListAsync();
            return devices;
        }

        public async Task<BaseDeviceModel> GetDeviceById(Guid id)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id)
                .Include(x => x.Alarms)
                .FirstOrDefaultAsync();
            return device;
        }

        public async Task<List<BaseDeviceModel>> GetDeviceByModelName(string modelName)
        {
            var devices = await _dbContext.Devices.Where(x => x.ModelName == modelName)
                .Include(x => x.Alarms)
                .ToListAsync();

            return devices;
        }

        public async Task<List<SnmpDeviceModel>> GetSnmpDeviceByBrandName(string brandName)
        {
            var devices = await _dbContext.Devices.Where(x => x.BrandName == brandName && x.CommunicationType == CommunicationType.SNMP)
                .Include(x => x.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<SnmpDeviceModel>>(devices);
            return response;
        }

        public async Task<SnmpDeviceModel> GetSnmpDeviceById(Guid id)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id)
                .Include(x => x.Alarms)
                .FirstOrDefaultAsync();

            var response = _mapper.Map<SnmpDeviceModel>(device);
            return response;

        }

        public async Task<List<TcpDeviceModel>> GetTcpDeviceByBrandName(string brandName)
        {
            var devices = await _dbContext.Devices.Where(x => x.BrandName == brandName && x.CommunicationType == CommunicationType.TCP)
                .Include(x => x.Alarms)
                .ToListAsync();

            var response = _mapper.Map<List<TcpDeviceModel>>(devices);
            return response;
        }

        public async Task<TcpDeviceModel> GetTcpDeviceById(Guid id)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id)
               .Include(x => x.Alarms)
               .FirstOrDefaultAsync();

            var response = _mapper.Map<TcpDeviceModel>(device);
            return response;
        }

        public async Task<bool> UpdateSnmpDevice(Guid id, SnmpDeviceAddModel updateModel)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id && x.CommunicationType == CommunicationType.SNMP)
                .Include(x => x.Alarms)
                .FirstOrDefaultAsync();

           if(device != null)
            {
                try
                {
                    _dbContext.Update(updateModel);
                    _dbContext.SaveChanges();
                    
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("ERROR : Device could not be update ---> ", ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> UpdateTcpDevice(Guid id, TcpDeviceAddModel updateModel)
        {
            var device = await _dbContext.Devices.Where(x => x.DeviceId == id && x.CommunicationType == CommunicationType.TCP)
                  .Include(x => x.Alarms)
                  .FirstOrDefaultAsync();

            if (device != null)
            {
                try
                {
                    _dbContext.Update(updateModel);
                    _dbContext.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR : Device could not be update ---> ", ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}
