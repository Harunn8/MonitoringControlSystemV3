using RuleApplication.Models;
using RuleApplication.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuleApplication.Services.Base
{
    public interface IPolicyScriptService
    {
        Task<List<ScriptResponse>> GetAllScript();
        Task<ScriptResponse> GetScriptById(Guid id);
        Task<List<ScriptResponse>> GetScriptByDateRange(DateTime startDate,DateTime endDate);
        Task CreateScript(ScriptModel script);
        Task UpdateScript(Guid id, ScriptModel script);
        Task DeleteScript(Guid id);
        Task<bool> RunPolicyScript(Guid id);
    }
}
