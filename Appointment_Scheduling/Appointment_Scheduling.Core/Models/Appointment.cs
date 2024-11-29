using Appointment_Scheduling.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public string NIN { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public ApplicationUser? Patient { get; set; }
        public ApplicationUser? Provider { get; set; }
    }
}
