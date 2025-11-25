using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Base;
using System.Threading.Tasks;
using System;
using Application.Models;
using McsCore.Entities;

namespace DeviceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TcpDeviceController : ControllerBase
    {
        private readonly ITcpDeviceService _tcpService;

        public TcpDeviceController(ITcpDeviceService tcpService)
        {
            _tcpService = tcpService;
        }

        [HttpGet("GetAllTcpDevice")]
        public async Task<ActionResult> GetAllTcpDevice()
        {
            var devices = await _tcpService.GetAllTcpDevice();
            if (devices == null || devices.Count == 0)
            {
                return NotFound("No TCP devices found.");
            }
            return Ok(devices);
        }

        [HttpGet("GetTcpDeviceById/{id}")]
        public async Task<ActionResult> GetTcpDeviceById(Guid id)
        {
            var device = await _tcpService.GetTcpDeviceById(id);
            if (device == null)
            {
                return NotFound("TCP Device not found.");
            }
            return Ok(device);
        }

        [HttpGet("GetTcpDeviceByName/{name}")]
        public async Task<ActionResult> GetTcpDeviceByName(string name)
        {
            var device = await _tcpService.GetTcpDeviceByName(name);
            if (device == null)
            {
                return NotFound("TCP Device not found.");
            }
            return Ok(device);
        }

        [HttpGet("GetTcpDeviceByIp/{ipAddress}")]
        public async Task<ActionResult> GetTcpDeviceByIp(string ipAddress)
        {
            var device = await _tcpService.GetTcpDeviceByIp(ipAddress);
            if (device == null)
            {
                return NotFound("TCP Device not found.");
            }
            return Ok(device);
        }

        [HttpGet("GetTcpDeviceByIpAndPort/{ipAddress}/{port}")]
        public async Task<ActionResult> GetTcpDeviceByIpAndPort(string ipAddress, int port)
        {
            var device = await _tcpService.GetTcpDeviceByIpAndPort(ipAddress, port);
            if (device == null)
            {
                return NotFound("TCP Device not found.");
            }
            return Ok(device);
        }

        [HttpGet("GetTcpDeviceByPort/{port}")]
        public async Task<ActionResult> GetTcpDeviceByPort(int port)
        {
            var device = await _tcpService.GetTcpDeviceByPort(port);
            if (device == null)
            {
                return NotFound("TCP Device not found.");
            }
            return Ok(device);
        }

        [HttpPost("CreateTcpDevice")]
        public async Task<ActionResult> AddTcpDevice([FromBody] TcpDeviceModel request)
        {
            var response = await _tcpService.AddTcpDevice(request);

            if (response == null) return BadRequest("Device can not added");

            return Ok(response);
        }

        [HttpPut("UpdateTcpDevice/{id}")]
        public async Task<ActionResult> UpdateTcpDevice(Guid id, [FromBody] TcpDeviceModel request)
        {
            var existingDevice = await _tcpService.GetTcpDeviceById(id);
            if (existingDevice == null)
            {
                return NotFound("TCP Device not found.");
            }
            await _tcpService.UpdateTcpDevice(id, request);
            return Ok(request);
        }

        [HttpDelete("DeleteTcpDevice/{id}")]
        public async Task<ActionResult> DeleteTcpDevice(Guid id)
        {
            var existingDevice = await _tcpService.GetTcpDeviceById(id);
            if (existingDevice == null)
            {
                return NotFound("TCP Device not found.");
            }
            await _tcpService.DeleteTcpDevice(id);
            return Ok("TCP Device deleted successfully.");
        }

        [HttpPost("StartTcpDeviceCommunication/{id}")]
        public async Task<ActionResult> StartTcpDeviceCommunication(Guid id)
        {
            var existingDevice = await _tcpService.GetTcpDeviceById(id);
            if (existingDevice == null)
            {
                return NotFound("TCP Device not found.");
            }
            await _tcpService.StartTcpDeviceCommunication(id);
            return Ok("TCP Device communication started successfully.");
        }
    }
}
