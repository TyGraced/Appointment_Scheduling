using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetProvidersAsync(bool trackChanges);
        Task<IEnumerable<ApplicationUser>> GetPatientsAsync(bool trackChanges);
        Task<ApplicationUser?> GetUserAsync(Guid userId, bool trackChanges);
    }
}
