using System.Text.Json.Serialization;

namespace Appointment_Scheduling.Core.DTOs
{
    public class SetAvailabilityRequest
    {
        public List<DayAvailability>? DailyAvailability { get; set; }
    }

    public class DayAvailability
    {
        public DayOfWeek DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
