using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
