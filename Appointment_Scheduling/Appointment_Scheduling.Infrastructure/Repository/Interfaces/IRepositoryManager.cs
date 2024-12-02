namespace Appointment_Scheduling.Infrastructure.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        IAppointmentRepository Appointment {  get; }
        IAvailabilityRepository Availability { get; }
        Task SaveAsync();
    }
}
