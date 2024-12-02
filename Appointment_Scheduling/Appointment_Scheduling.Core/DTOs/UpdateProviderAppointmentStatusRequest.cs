using Appointment_Scheduling.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.DTOs
{
    public class UpdateProviderAppointmentStatusRequest
    {
        [Required]
        public Guid PatientId { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
