using McsCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MonitorApplication.Models;
using MonitorApplication.Services.Base;
using System;
using System.Threading.Tasks;

namespace MonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamaterLogController : ControllerBase
    {
        private readonly IParameterLogService _parameterLogService;

        public ParamaterLogController(IParameterLogService paramaterLogService)
        {
            _parameterLogService = paramaterLogService;
        }

        [HttpGet("getbypage")]
        public async Task<ActionResult> GetParameterLogsByPage([FromQuery] TableModel tableModel)
        {
            var result = await _parameterLogService.GetParameterLogsByPage(tableModel);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddParameterLog([FromBody] ParameterLogsAdd parameterLog)
        {
            var response = await _parameterLogService.AddParameterLogs(parameterLog);
            return Ok(response);
        }

        [HttpPost("startorstop")]
        public async Task<ActionResult> ParameterSetsStartOrStop(Guid parameterSetsId, bool isActive)
        {
            var response = await _parameterLogService.StartOrStopParameterLogs(parameterSetsId, isActive);
            return Ok(response);
        }
    }
}
