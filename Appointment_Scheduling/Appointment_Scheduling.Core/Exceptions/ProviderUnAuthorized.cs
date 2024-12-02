namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class ProviderUnAuthorized : UnauthorizedAccessException
    {
        public ProviderUnAuthorized()
            : base("Sorry, only Providers are authorized to set availability.") { }
    }
}
