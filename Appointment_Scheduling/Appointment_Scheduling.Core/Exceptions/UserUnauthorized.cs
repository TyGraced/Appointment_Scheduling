namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class UserUnauthorized : UnauthorizedAccessException
    {
        public UserUnauthorized()
            : base("Sorry, you are not authorized. You should login.") { }
    }
}
