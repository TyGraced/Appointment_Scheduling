namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class TimeMismatchException : BadRequestException
    {
        public TimeMismatchException()
            : base("Start time must be earlier than end time.") { }
    }
}
