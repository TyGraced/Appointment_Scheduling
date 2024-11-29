using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.Models
{
    public class ProviderAvailability
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public ApplicationUser? Provider { get; set; }

    }
}
