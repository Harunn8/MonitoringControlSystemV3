using AutoMapper;
using McsCore.AppDbContext;
using McsMqtt.Producer;
using McsUserLogs.Services.Base;
using Microsoft.EntityFrameworkCore;
using Redis.Services;
using RuleApplication.Models;
using RuleApplication.Responses;
using RuleApplication.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RuleApplication.Services
{
    public class PolicyScriptService : IPolicyScriptService
    {
        private readonly MqttProducer _mqtt;
        private readonly McsAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserLogService _userLog;

        public PolicyScriptService(MqttProducer mqtt, McsAppDbContext dbContext, IMapper mapper, IUserLogService userLog)
        {
            _mqtt = mqtt;
            _dbContext = dbContext;
            _mapper = mapper;
            _userLog = userLog;
        }

        public async Task CreateScript(ScriptModel script)
        {
            await _dbContext.Scripts.AddAsync(script);
            await _dbContext.SaveChangesAsync();
            await _userLog.SetEventUserLog(new McsCore.Entities.UserLogs
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AppName = "RE/PolicyScriptService/AddPolicyScript",
                Message = $"Create new script: {script.ScriptName}",
                LogDate = DateTime.UtcNow,
                LogType = McsCore.Entities.UserLogType.Added
            });
        }

        public async Task DeleteScript(Guid id)
        {
            var script = await _dbContext.Scripts.FirstOrDefaultAsync(x => x.Id == id);
            if (script != null)
            {
                _dbContext.Scripts.Remove(script);
                await _dbContext.SaveChangesAsync();

                await _userLog.SetEventUserLog(new McsCore.Entities.UserLogs
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    AppName = "RE/PolicyScriptService/DeletePolicyScript",
                    Message = $"Delete script: {script.ScriptName}",
                    LogDate = DateTime.UtcNow,
                    LogType = McsCore.Entities.UserLogType.Deleted
                });
            }
        }

        public async Task<List<ScriptResponse>> GetAllScript()
        {
            var scripts = await _dbContext.Scripts.ToListAsync();
            var response = _mapper.Map<List<ScriptResponse>>(scripts);
            return response;
        }

        public async Task<List<ScriptResponse>> GetScriptByDateRange(DateTime startDate, DateTime endDate)
        {
            var scripts = await _dbContext.Scripts.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate).ToListAsync();
            var response = _mapper.Map<List<ScriptResponse>>(scripts);
            return response;
        }

        public async Task<ScriptResponse> GetScriptById(Guid id)
        {
            var script = await _dbContext.Scripts.FirstOrDefaultAsync(x => x.Id == id);
            var response = _mapper.Map<ScriptResponse>(script);
            return response;
        }

        public async Task<bool> RunPolicyScript(Guid id)
        {
            var script = await GetScriptById(id);
            if (script != null)
            {
                var sendingScript = JsonSerializer.Serialize(script);
                _mqtt.PublishMessage("policyScript/start", $"{sendingScript}");
                return true;
            }
            return false;
        }

        public async Task UpdateScript(Guid id, ScriptModel script)
        {
            var existingScript = await _dbContext.Scripts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingScript != null)
            {
                _dbContext.Scripts.Update(script);
                await _dbContext.SaveChangesAsync();
                await _userLog.SetEventUserLog(new McsCore.Entities.UserLogs
                {
                    Id = id,
                    UserId = Guid.NewGuid(),
                    AppName = "RE/PolicyScriptService/UpdatePolicyScript",
                    Message = $"Update script: {script.ScriptName}",
                    LogDate = DateTime.UtcNow,
                    LogType = McsCore.Entities.UserLogType.Updated
                });
            }
        }
    }
}
