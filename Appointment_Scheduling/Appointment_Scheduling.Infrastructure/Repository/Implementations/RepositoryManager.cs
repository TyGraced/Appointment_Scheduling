using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
