using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Base;
using System.Threading.Tasks;
using System;
using Application.Models;
using McsCore.Entities;
using DeviceApplication.Models;
using Application.Responses;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnmpDeviceController : ControllerBase
    {
        private readonly ISnmpDeviceService _snmpService;

        public SnmpDeviceController(ISnmpDeviceService snmpDeviceService)
        {
            _snmpService = snmpDeviceService;
        }

        [HttpGet("GetAllSnmpDevice")]
        public async Task<IActionResult> GetAllSnmpDevice()
        {
            return Ok(await _snmpService.GetAllSnmpDevice());
        }

        [HttpGet("GetSnmpDeviceById/{id}")]
        public async Task<IActionResult> GetSnmpDeviceById(Guid id)
        {
            var result = await _snmpService.GetSnmpDeviceById(id);
            if (result == null)
            {
                return NotFound("Device Not Found");
            }
            return Ok(result);
        }

        [HttpGet("GetSnmpDeviceByName/{name}")]
        public async Task<IActionResult> GetSnmpDeviceByName(string name)
        {
            var result = await _snmpService.GetSnmpDeviceByName(name);
            if (result == null)
            {
                return NotFound("Device Not Found");
            }
            return Ok(result);
        }

        [HttpGet("GetSnmpDeviceByIp/{ipAddress}")]
        public async Task<IActionResult> GetSnmpDeviceByIp(string ipAddress)
        {
            var result = await _snmpService.GetSnmpDeviceByIp(ipAddress);
            if (result == null)
            {
                return NotFound("Device Not Found");
            }
            return Ok(result);
        }

        [HttpGet("GetSnmpDeviceByPort/{port}")]
        public async Task<IActionResult> GetSnmpDeviceByPort(int port)
        {
            var result = await _snmpService.GetSnmpDeviceByPort(port);
            if (result == null)
            {
                return NotFound("Device Not Found");
            }
            return Ok(result);
        }

        [HttpGet("GetSnmpDeviceByIpAndPort/{ip},{port}")]
        public async Task<IActionResult> GetSnmpDeviceByIpAndPort(string ip, int port)
        {
            var result = await _snmpService.GetSnmpDeviceByIpAndPort(ip, port);
            if (result == null)
            {
                return NotFound("Device Not Found");
            }
            return Ok(result);
        }

        [HttpPost("AddSnmpDevice")]
        public async Task<SnmpDeviceResponses> AddSnmpDevice([FromBody] SnmpDevice snmpDevice)
        {
            if (snmpDevice == null) return null;
            
            var result = await _snmpService.AddSnmpDevice(snmpDevice);
            
            if (result == null) return null;
            
            return result;
        }

        [HttpPut("UpdateSnmpDevice")]
        public async Task<IActionResult> UpdateSnmpDevice(Guid id, [FromBody] SnmpDevice snmpDevice)
        {
            var device = await _snmpService.GetSnmpDeviceById(id);
            if (device == null)
            {
                return NotFound("Device Not Found");
            }
            await _snmpService.UpdateSnmpDevice(id, snmpDevice);
            return Ok("Device could update");
        }

        [HttpDelete("DeleteSnmpDevice/{id}")]
        public async Task<IActionResult> DeleteSnmpDevice(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid SNMP Device ID.");
            }

            var result = _snmpService.DeleteSnmpDevice(id);
            return Ok("Device could delete");
        }

        [HttpPost("StartSnmpCommunication/{id}")]
        public async Task<ActionResult> StartSnmpCommunication(Guid id)
        {
            var device = await _snmpService.GetSnmpDeviceById(id);
            if (device == null)
            {
                return NotFound("Device Not Found");
            }
            await _snmpService.StartSnmpCommunication(id);
            return Ok("SNMP Communication started successfully.");
        }

        [HttpPost("StopSnmpCommunication/{id}")]
        public async Task<ActionResult> StopSnmpCommunication(Guid id)
        {
            var device = await _snmpService.GetSnmpDeviceById(id);
            if (device == null)
            {
                return NotFound("Device Not Found");
            }
            await _snmpService.StopSnmpCommunication(id);
            return Ok("SNMP Communication stopped successfully.");
        }

        [HttpPost("SendSnmpCommand")]
        public async Task<IActionResult> SendSnmpCommand([FromBody] SnmpCommandModel snmpCommandModel)
        {
            if (snmpCommandModel == null)
            {
                return BadRequest("Invalid SNMP Command data.");
            }
            var result = await _snmpService.SendSnmpCommand(snmpCommandModel);
            if (result == null)
            {
                return BadRequest("Failed to send SNMP Command.");
            }
            return Ok(result);
        }
    }
}
