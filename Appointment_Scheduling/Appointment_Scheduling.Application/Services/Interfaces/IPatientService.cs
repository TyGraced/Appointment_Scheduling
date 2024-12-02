using Appointment_Scheduling.Core.DTOs;
using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface IPatientService
    {
        Task<ApiResponse<ProviderAvailability>> GetAvailabilityAsync(DateTime dateTime, Guid providerId, bool trackChanges);
        Task<ApiResponse<string>> CancelAppointmentAsync(Guid providerId);
        Task<ApiResponse<string>> BookAppointmentAsync(BookAppointmentRequest request);
    }
}
