namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class AdminUnauthorized : UnauthorizedAccessException
    {
        public AdminUnauthorized()
            : base("Sorry, only Admins can access this.") { }
    }
}

