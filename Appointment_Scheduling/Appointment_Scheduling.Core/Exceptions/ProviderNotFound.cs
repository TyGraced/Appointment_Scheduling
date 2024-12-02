namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class ProviderNotFound : NotFoundException
    {
        public ProviderNotFound()
            : base("This Provider is unavailable") { }
    }
}
