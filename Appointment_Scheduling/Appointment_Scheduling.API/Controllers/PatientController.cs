using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduling.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PatientController(IServiceManager service) => _service = service;


        [HttpPost("book-appointment")]
        public async Task<IActionResult> BookAppointmentAsync([FromBody] BookAppointmentRequest request)
        {
            var result = await _service.PatientService.BookAppointmentAsync(request);
            return Ok(result);
        }

        [HttpPut("{providerId}/appointment")]
        public async Task<IActionResult> UpdateAppointmentStatus(Guid providerId)
        {
            var result = await _service.PatientService.CancelAppointmentAsync(providerId);
            return Ok(result);
        }

        [HttpGet("provider-availability")]
        public async Task<IActionResult> GetAvailabilityAsync(DateTime dateTime, Guid providerId)
        {
            var result = await _service.PatientService.GetAvailabilityAsync(dateTime, providerId, trackChanges: false);
            return Ok(result);
        }
    }
}
