using Appointment_Scheduling.Core.Models;

namespace Appointment_Scheduling.Infrastructure.Repository.Interfaces
{
    public interface IAvailabilityRepository
    {
        // Provider
        Task SetAvailabilityAsync(ProviderAvailability availability);


        // Patient
        Task<ProviderAvailability?> GetAvailabilityAsync(DateTime dateTime, Guid providerId, bool trackChanges);
        Task<IEnumerable<ProviderAvailability?>> GetAvailabilityAsync(Guid providerId, bool trackChanges);
    }
}
