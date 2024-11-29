using Appointment_Scheduling.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.DTOs
{
    public class AppointmentRequest
    {

        [Required]
        public Guid ProviderId { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
