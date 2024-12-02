using Appointment_Scheduling.Core.DTOs;
using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface IProviderService
    {
        Task<ApiResponse<string>> SetAvailabilityAsync(SetAvailabilityRequest request);
        Task<ApiResponse<Appointment>> GetAppointmentAsync(Guid patientId, bool trackChanges);
        Task<ApiResponse<string>> UpdateAppointmentStatus(UpdateProviderAppointmentStatusRequest request);
        Task<ApiResponse<IEnumerable<Appointment>>> GetAppointmentsAsync(bool trackChanges);
    }
}
