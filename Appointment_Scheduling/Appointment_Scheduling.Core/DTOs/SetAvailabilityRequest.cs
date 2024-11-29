namespace Appointment_Scheduling.Core.DTOs
{
    public class SetAvailabilityRequest
    {
        public Guid ProviderId { get; set; }
        public List<DayAvailability>? DailyAvailability { get; set; }
    }

    public class DayAvailability
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
