using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IAvailabilityRepository> _availabilityRepository;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(context));
            _availabilityRepository = new Lazy<IAvailabilityRepository>(() => new AvailabilityRepository(context));
        }

        public IAppointmentRepository Appointment => _appointmentRepository.Value;

        public IAvailabilityRepository Availability => _availabilityRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
