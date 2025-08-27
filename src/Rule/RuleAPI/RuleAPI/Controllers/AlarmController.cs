using Application.Models;
using McsMqtt.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuleApplication.Services.Base;
using Serilog;
using System;
using System.Threading.Tasks;

namespace RuleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmController : ControllerBase
    {
        private readonly IAlarmService _alarmService;
        private readonly MqttProducer _mqtt;

        public AlarmController(IAlarmService alarmService, MqttProducer mqtt)
        {
            _alarmService = alarmService;
            _mqtt = mqtt;
        }

        [HttpGet("GetAllAlarm")]
        public async Task<ActionResult> GetAllAlarm()
        {
            var result = await _alarmService.GetAllAlarm();
            return Ok(result);
        }

        [HttpGet("GetAlarmById/{id}")]
        public async Task<ActionResult> GetAlarmById(Guid id)
        {
            var result = await _alarmService.GetAlarmById(id);
            return Ok(result);
        }

        [HttpGet("GetAlarmByDateRange")]
        public async Task<ActionResult> GetAlarmByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = await _alarmService.GetAlarmByDateRange(startDate, endDate);
            return Ok(result);
        }

        [HttpGet("GetAlarmByStatus/{status}")]
        public async Task<ActionResult> GetAlarmByStatus(int status)
        {
            var result = await _alarmService.GetAlarmByStatus((McsCore.Entities.Severity)status);
            return Ok(result);
        }

        [HttpGet("GetAlarmByDeviceId/{deviceId}")]
        public async Task<ActionResult> GetAlarmByDeviceId(Guid deviceId)
        {
            var result = await _alarmService.GetAlarmByDeviceId(deviceId);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound("Data not found");
        }

        [HttpGet("GetAlarmByParameterId/{parameterId}")]
        public async Task<ActionResult> GetAlarmByParameterId(Guid parameterId)
        {
            var result = await _alarmService.GetAlarmByParameterId(parameterId);
            return Ok(result);
        }

        [HttpPost("AddAlarm")]
        public async Task<ActionResult> AddAlarm([FromBody] Application.Models.AlarmModel alarm)
        {
            try
            {
                await _alarmService.AddAlarm(alarm);
                _mqtt.PublishMessage("alarm/created", $"Alarm created with ID: {alarm.Id}");
                return Ok("Alarm added successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding alarm");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding alarm");
            }
        }

        [HttpPut("UpdateAlarm/{id}")]
        public async Task<ActionResult> UpdateAlarm(Guid id, [FromBody] AlarmModel alarm)
        {
            try
            {
                await _alarmService.UpdateAlarm(id, alarm);
                _mqtt.PublishMessage("alarm/updated", $"Alarm updated with ID: {id}");
                return Ok("Alarm updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating alarm");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating alarm");
            }
        }
    }
}