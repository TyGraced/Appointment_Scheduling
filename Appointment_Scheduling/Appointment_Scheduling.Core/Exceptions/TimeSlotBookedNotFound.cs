namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class TimeSlotBookedNotFound : NotFoundException
    {
        public TimeSlotBookedNotFound()
            : base("This time slot is already booked.") { }
    }
}
