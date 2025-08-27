using Microsoft.AspNetCore.Mvc;
using RuleApplication.Models;
using RuleApplication.Services.Base;
using System;
using System.Threading.Tasks;

namespace RuleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyScriptController : ControllerBase
    {
        private readonly IPolicyScriptService _policyScriptService;

        public PolicyScriptController(IPolicyScriptService policyScriptService)
        {
            _policyScriptService = policyScriptService;
        }

        [HttpGet("GetAllScript")]
        public async Task<ActionResult> GetAllScript()
        {
            var scripts = await _policyScriptService.GetAllScript();
            return Ok(scripts);
        }

        [HttpGet("GetScriptById/{id}")]
        public async Task<ActionResult> GetScriptById(Guid id)
        {
            var script = await _policyScriptService.GetScriptById(id);
            if (script != null)
            {
                return Ok(script);
            }
            else
            {
                return NotFound("Script not found");
            }
        }

        [HttpGet("GetScriptByDateRange")]
        public async Task<ActionResult> GetScriptByDateRange(DateTime startDate, DateTime endDate)
        {
            var scripts = await _policyScriptService.GetScriptByDateRange(startDate, endDate);
            return Ok(scripts);
        }

        [HttpPost("CreateScript")]
        public async Task<ActionResult> CreateScript([FromBody] ScriptModel model)
        {
            // Validation eklenecek...

            try
            {
                await _policyScriptService.CreateScript(model);
                return Ok("Alarm added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Script could not add: {ex.Message}");
            }
        }

        [HttpPut("UpdateScript/{id}")]
        public async Task<ActionResult> UpdateScript(Guid id, [FromBody] ScriptModel model)
        {
            // Validation eklenecek...
            try
            {
                await _policyScriptService.UpdateScript(id, model);
                return Ok("Script updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Script could not update: {ex.Message}");
            }
        }
    }
}