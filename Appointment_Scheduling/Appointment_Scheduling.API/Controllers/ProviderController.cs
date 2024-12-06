using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduling.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProviderController(IServiceManager service) => _service = service;


        [HttpPost("set-availability")]
        public async Task<IActionResult> SetAvailability([FromBody] SetAvailabilityRequest request)
        {
            var result = await _service.ProviderService.SetAvailabilityAsync(request);
            return Ok(result);
        }
        
        [HttpPut("update-appointment-status")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAppointmentStatus([FromBody] UpdateProviderAppointmentStatusRequest request)
        {
            var result = await _service.ProviderService.UpdateAppointmentStatus(request, trackChanges:false);
            return Ok(result);
        }

        [HttpGet("appointments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAppointments()
        {
            var result = await _service.ProviderService.GetAppointmentsAsync(trackChanges: false);
            return Ok(result);
        }

        [HttpGet("{patientId}/appointment")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAppointment(Guid patientId)
        {
            var result = await _service.ProviderService.GetAppointmentAsync(patientId, trackChanges:false);
            return Ok(result);
        }

    }
}
