using DeviceApplication.Models;
using DeviceApplication.Services.Base;
using McsCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagDeviceController : ControllerBase
    {
        private readonly IPagDeviceService _pagDeviceService;

        public PagDeviceController(IPagDeviceService pagDeviceService)
        {
            _pagDeviceService = pagDeviceService;
        }

        [HttpGet("getallpagdevice")]
        public async Task<ActionResult> GetAllPagDevices()
        {
            var response = await _pagDeviceService.GetAllPagDevice();

            if (response == null) return NotFound("Pag Device not found");
            return Ok(response);
        }

        [HttpGet("getpagdevicebyid")]
        public async Task<ActionResult> GetPagDeviceById(Guid id)
        {
            var response = await _pagDeviceService.GetPagDeviceById(id);

            if (response == null) return NotFound("Pag Device not found");
            return Ok(response);
        }

        [HttpGet("gettcpdevicebydeviceid")]
        public async Task<ActionResult> GetTcpDeviceByDeviceId(Guid deviceId)
        {
            var response = await _pagDeviceService.GetPagDeviceByDeviceId(deviceId);

            if (response == null) return NotFound("Pag Device not found");
            return Ok(response);
        }

        [HttpGet("gettcpdevicebypagid")]
        public async Task<ActionResult> GetTcpDeviceByPagId(Guid pagId)
        {
            var response = await _pagDeviceService.GetPagDeviceByPagId(pagId);

            if (response == null) return NotFound("Pag Device not found");
            return Ok(response);
        }

        [HttpPost("addpagdevice")]
        public async Task<ActionResult> AddPagDevice(PagDeviceAddModel pagDeviceModel)
        {
            var response = await _pagDeviceService.AddPagDevice(pagDeviceModel);

            if (response) return Ok(response);
            return BadRequest("Pag Device could not be add");
        }

        [HttpPut("updatepagdevice")]
        public async Task<ActionResult> UpdatePagDevice(Guid pagDeviceId, PagDevices updatePagDeviceModel)
        {
            var response = await _pagDeviceService.UpdatePagDevice(pagDeviceId, updatePagDeviceModel);

            if (response) return Ok("Pag Device updated successful");
            return BadRequest("Pag Device could not be update");
        }

        [HttpDelete("deletepagdevice")]
        public async Task<ActionResult> DeletePagDevice(Guid pagDeviceId)
        {
            var response = await _pagDeviceService.DeletePagDevice(pagDeviceId);

            if (response) return Ok("Pag Device deleted successful");
            return BadRequest("Pag Device could not be delete");
        }

        [HttpPut("startorstoppagdevicecommunication")]
        public async Task<ActionResult> StartOrStopCommunication(Guid pagDeviceId, bool isActive)
        {
            var response = await _pagDeviceService.StartOrStopCommunication(pagDeviceId, isActive);

            if (response) return Ok("Device Communication Started");
            return BadRequest("Device Communication could not start");
        }
    }
}
