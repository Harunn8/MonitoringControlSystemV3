using AutoMapper;
using DeviceApplication.Repositories.Base;
using DeviceApplication.Responses;
using McsCore.AppDbContext;
using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApplication.Repositories
{
    public class PagRepository : IPagRepository
    {
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PagRepository(McsAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> AddPag(Pags pagModel)
        {
            pagModel.Id = Guid.NewGuid();

            try
            {
                await _dbContext.Pags.AddAsync(pagModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR : Pag could not be add -----> ",ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePag(Guid id)
        {
            var pag = await GetPagById(id);

            if(pag != null)
            {
                var entity = _mapper.Map<Pags>(pag);

                try
                {
                    _dbContext.Pags.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR : Pag could not be delete -----> ", ex.Message);
                    return false;
                }
            }
            return false;
        }

        public async Task<List<PagResponse>> GetAllPag()
        {
            var pags = await _dbContext.Pags.ToListAsync();
            var response = _mapper.Map<List<PagResponse>>(pags);
            return response;
        }

        public async Task<PagResponse> GetPagById(Guid id)
        {
            var pags = await _dbContext.Pags.Where(x => x.Id == id).FirstOrDefaultAsync();
            var response = _mapper.Map<PagResponse>(pags);
            return response;
        }

        public async Task<PagResponse> GetPagByName(string name)
        {
            var pag = await _dbContext.Pags.Where(x => x.Name == name).FirstOrDefaultAsync();
            var response = _mapper.Map<PagResponse>(pag);
            return response;
        }

        public async Task<bool> UpdatePag(Guid id, Pags pagModel)
        {
            var pag = await GetPagById(id);

            if(pag != null)
            {
                _dbContext.Pags.Update(pagModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
