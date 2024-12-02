using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.DTOs
{
    public class BookAppointmentRequest
    {
        [Required]
        public Guid ProviderId { get; set; }
        [Required]
        public DateTime AppointmentStartTime { get; set; }
        [Required]
        public DateTime AppointmentEndTime { get; set; }
        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
