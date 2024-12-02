namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class TimeSlotNotFound : NotFoundException
    {
        public TimeSlotNotFound()
            : base("No available time slot for the selected date and time.") { }
    }
}




