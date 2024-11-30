namespace Appointment_Scheduling.Core.Exceptions
{
    public sealed class AppointmentNotFound : NotFoundException 
    {
        public AppointmentNotFound(Guid patientId)
            :base($"Appointment with patientId: {patientId} doesn't exist.") { }
    }
}
