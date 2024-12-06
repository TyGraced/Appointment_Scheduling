using Appointment_Scheduling.Infrastructure.Data;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Appointment_Scheduling.Infrastructure.Repository.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IAvailabilityRepository> _availabilityRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRepositoryBase<IdentityUserRole<Guid>>> _userRoleRepository;
        private readonly Lazy<IRepositoryBase<IdentityRole>> _roleRepository;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(context));
            _availabilityRepository = new Lazy<IAvailabilityRepository>(() => new AvailabilityRepository(context));
            _userRoleRepository = new Lazy<IRepositoryBase<IdentityUserRole<Guid>>>(() => new RepositoryBase<IdentityUserRole<Guid>>(context));
            _roleRepository = new Lazy<IRepositoryBase<IdentityRole>>(() => new RepositoryBase<IdentityRole>(context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(context, _userRoleRepository.Value, _roleRepository.Value));

        }

        public IAppointmentRepository Appointment => _appointmentRepository.Value;

        public IAvailabilityRepository Availability => _availabilityRepository.Value;

        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
