using Appointment_Scheduling.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduling.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AdminController(IServiceManager service) => _service = service;


        [HttpGet("all-appointments")]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            var result = await _service.AdminService.GetAllAppointmentsAsync(trackChanges:false);
            return Ok(result);
        }

        [HttpGet("cancelled-appointments")]
        public async Task<IActionResult> GetCancelledAppointmentsAsync()
        {
            var result = await _service.AdminService.GetCancelledAppointmentsAsync(trackChanges:false);
            return Ok(result);
        }

        [HttpGet("pending-appointments")]
        public async Task<IActionResult> GetPendingAppointmentsAsync()
        {
            var result = await _service.AdminService.GetPendingAppointmentsAsync(trackChanges:false);
            return Ok(result);
        }

        [HttpGet("confirmed-appointments")]
        public async Task<IActionResult> GetConfirmedAppointmentsAsync()
        {
            var result = await _service.AdminService.GetConfirmedAppointmentsAsync(trackChanges:false);
            return Ok(result);
        }

        [HttpGet("completed-appointments")]
        public async Task<IActionResult> GetCompletedAppointmentsAsync()
        {
            var result = await _service.AdminService.GetCompletedAppointmentsAsync(trackChanges:false);
            return Ok(result);
        }
    }
}
