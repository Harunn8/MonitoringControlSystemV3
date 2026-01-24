using DeviceApplication.Services.Base;
using McsCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagController : ControllerBase
    {
        private readonly IPagService _pagService;

        public PagController(IPagService pagService)
        {
            _pagService = pagService;
        }

        [HttpGet("getallpags")]
        public async Task<ActionResult> GetAllPags()
        {
            var response = await _pagService.GetAllPag();

            if (response != null) return Ok(response);
            return NotFound("Pag not found");
        }

        [HttpGet("getpagbyid")]
        public async Task<ActionResult> GetPagById(Guid id)
        {
            var response = await _pagService.GetPagById(id);

            if (response != null) return Ok(response);
            return NotFound("Pag not found");
        }

        [HttpGet("getpagbyname")]
        public async Task<ActionResult> GetPagByName(string name)
        {
            var response = await _pagService.GetPagByName(name);

            if (response != null) return Ok(response);
            return NotFound("Pag not found");
        }

        [HttpPost("addpag")]
        public async Task<ActionResult> AddPag(Pags pagModel)
        {
            var response = await _pagService.AddPag(pagModel);

            if(response) return Ok(response);
            return BadRequest("Pag could not be add");
        }

        [HttpPut("updatepag")]
        public async Task<ActionResult> UpdatePag(Guid id, Pags updatePagModel)
        {
            var response = await _pagService.UpdatePag(id, updatePagModel);

            if (response) return Ok(response);
            return BadRequest("Pag could not be update");
        }

        [HttpDelete("deletepag")]
        public async Task<ActionResult> DeletePag(Guid id)
        {
            var response = await _pagService.DeletePag(id);

            if (response) return Ok(response);
            return BadRequest("Pag could not be delete");
        }
    }
}
