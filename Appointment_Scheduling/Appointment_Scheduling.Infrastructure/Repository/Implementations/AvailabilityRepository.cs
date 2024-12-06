using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class AvailabilityRepository : RepositoryBase<ProviderAvailability>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Patient
        public async Task<ProviderAvailability?> GetAvailabilityAsync(DateTime dateTime, Guid providerId, bool trackChanges)
        {
            var dayOfWeek = dateTime.DayOfWeek;
            var timeOfDay = dateTime.TimeOfDay;

            return await FindByCondition(p =>
                    p.ProviderId == providerId &&
                    p.DayOfWeek == dayOfWeek &&
                    p.StartTime <= timeOfDay &&
                    p.EndTime >= timeOfDay.Add(TimeSpan.FromMinutes(30)),
                    trackChanges)
                .SingleOrDefaultAsync();
        }

        public Task<IEnumerable<ProviderAvailability?>> GetAvailabilityAsync(Guid providerId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        // Provider
        public async Task SetAvailabilityAsync(ProviderAvailability availability)
        {
            Create(availability);
        }
    }
}
