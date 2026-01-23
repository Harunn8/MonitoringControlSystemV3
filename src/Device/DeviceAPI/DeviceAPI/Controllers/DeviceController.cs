using DeviceApplication.Models;
using DeviceApplication.Services.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Threading.Tasks;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("getalldevice")]
        public async Task<ActionResult> GetAllDevice()
        {
            var response = await _deviceService.GetAllDevice();
            if(response.Count == 0) return NotFound("Devices not found");
            return Ok(response);
        }

        [HttpGet("getdevicebyid")]
        public async Task<ActionResult> GetDeviceById(Guid id)
        {
            var response = await _deviceService.GetSnmpDeviceById(id);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("getdevicebybradname")]
        public async Task<ActionResult> GetDeviceByBrandName(string brandName)
        {
            var response = await _deviceService.GetDeviceByBrandName(brandName);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("getdevicebymodelname")]
        public async Task<ActionResult> GetDeviceByModelName(string modelName)
        {
            var response = await _deviceService.GetDeviceByModelName(modelName);
            
            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("getdevicebycategoryname")]
        public async Task<ActionResult> GetDeviceByCategoryName(string categoryName)
        {
            var response = await _deviceService.GetDeviceByCategoryName(categoryName);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpDelete("deletedevice")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            var response = await _deviceService.DeleteDevice(id);

            if (response) return Ok("Device deleted");
            return BadRequest("Device could not deleted");
        }

        [HttpGet("getallsnmpdevice")]
        public async Task<ActionResult> GetAllSnmpDevices()
        {
            var response = await _deviceService.GetAllSnmpDevice();

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("getsnmpdevicebyid")]
        public async Task<ActionResult> GetSnmpDeviceById(Guid id)
        {
            var response = await _deviceService.GetSnmpDeviceById(id);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("getsnmpdevicebybrandname")]
        public async Task<ActionResult> GetSnmpDeviceByBrandName(string brandName)
        {
            var response = await _deviceService.GetSnmpDeviceByBrandName(brandName);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpPost("addsnmpdevice")]
        public async Task<ActionResult> AddSnmpDevice(SnmpDeviceAddModel snmpDeviceModel)
        {
            var response = await _deviceService.AddSnmpDevice(snmpDeviceModel);

            if(response) return Ok("Device added");
            return BadRequest("Device could not deleted");
        }

        [HttpPut("updatesnmpdevice")]
        public async Task<ActionResult> UpdateSnmpDevice(Guid id, SnmpDeviceAddModel updateSnmpDeviceModel)
        {
            var response = await _deviceService.UpdateSnmpDevice(id, updateSnmpDeviceModel);

            if (response) return Ok("Device updated");
            return BadRequest("Device could not update");
        }

        [HttpGet("getalltcpdevice")]
        public async Task<ActionResult> GetAllTcpDevice()
        {
            var response = await _deviceService.GetAllTcpDevice();

            if (response != null) return Ok(response);
            return NotFound("Device not found");
        }

        [HttpGet("gettcpdevicebyid")]
        public async Task<ActionResult> GetTcpDeviceById(Guid id)
        {
            var response = await _deviceService.GetTcpDeviceById(id);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpGet("gettcpdevicebybrandname")]
        public async Task<ActionResult> GetTcpDeviceByBrandName(string brandName)
        {
            var response = await _deviceService.GetTcpDeviceByBrandName(brandName);

            if (response == null) return NotFound("Device not found");
            return Ok(response);
        }

        [HttpPost("addtcpdevice")]
        public async Task<ActionResult> AddTcpDevice(TcpDeviceAddModel addTcpDeviceModel)
        {
            var response = await _deviceService.AddTcpDevice(addTcpDeviceModel);

            if (!response) return BadRequest("Device could not be add");
            return Ok(response);
        }

        [HttpPut("updatetcpdevice")]
        public async Task<ActionResult> UpdateTcpDevice(Guid id, TcpDeviceAddModel updateTcpDeviceModel)
        {
            var response = await _deviceService.UpdateTcpDevice(id, updateTcpDeviceModel);

            if (!response) return BadRequest("Device could not be update");
            return Ok(response);
        }
    }
}
