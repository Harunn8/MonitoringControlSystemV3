using McsCore.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using MonitorApplication.Services.Base;
using Swashbuckle.Swagger.Annotations;
using System.Threading.Tasks;

namespace MonitorAPI.Controllers
{
    [Route("api/Controller")]
    [ApiController]
    public class ParamaterLogController : ControllerBase
    {
        private readonly IParameterLogService _parameterLogService;

        public ParamaterLogController(IParameterLogService paramaterLogService)
        {
            _parameterLogService = paramaterLogService;
        }

        [HttpGet]
        [SwaggerOperation("Tüm logları getirir")]
        public async Task<ActionResult> GetParameterLogsByPage([FromQuery] TableModel tableModel)
        {
            var result = await _parameterLogService.GetParameterLogsByPage(tableModel);
            return Ok(result);
        }
    }
}
