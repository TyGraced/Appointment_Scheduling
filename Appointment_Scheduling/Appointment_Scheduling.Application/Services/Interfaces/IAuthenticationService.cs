using Appointment_Scheduling.Core.DTOs;

namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterRequest request);
        Task<ApiResponse<AuthenticationResult>> LoginAsync(LoginRequest request);
        Task<ApiResponse<string>> LogoutAsync();
    }
}
