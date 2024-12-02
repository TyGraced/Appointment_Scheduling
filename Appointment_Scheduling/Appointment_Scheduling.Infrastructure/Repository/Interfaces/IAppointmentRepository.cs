using Appointment_Scheduling.Core.Enums;
using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Infrastructure.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        // Admin
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(bool trackChanges);
        Task<IEnumerable<Appointment>> GetCancelledAppointmentsAsync(bool trackChanges);
        Task<IEnumerable<Appointment>> GetConfirmedAppointmentsAsync(bool trackChanges);
        Task<IEnumerable<Appointment>> GetCompletedAppointmentsAsync(bool trackChanges);
        Task<IEnumerable<Appointment>> GetPendingAppointmentsAsync(bool trackChanges);

        // Provider
        Task<Appointment?> GetAppointmentAsync(Guid patientId, Guid providerId, bool trackChanges);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync(Guid providerId, bool trackChanges);
        Task UpdateAppointmentStatusAsync(Guid patientId, Guid providerId, AppointmentStatus status);

        // Patient
        Task BookAppointment(Appointment appointment);
        Task CancelAppointmentAsync(Guid patientId, Guid providerId);
        Task<Appointment?> GetOverlappingAppointmentAsync(Guid providerId,
            DateTime newStartTime, DateTime newEndTime, bool trackChanges);
    }
}
