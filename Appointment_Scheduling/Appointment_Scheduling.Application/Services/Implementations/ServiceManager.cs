using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Core.Settings;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Appointment_Scheduling.Application.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ITokenService> _tokenService;
        private readonly Lazy<IProviderService> _providerService;
        private readonly Lazy<IAdminService> _adminService;
        private readonly Lazy<IPatientService> _patientService;
        public ServiceManager(IRepositoryManager repositoryManager, IHttpContextAccessor _contextAccessor,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> configuration, ITokenService tokenService)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                    new AuthenticationService(userManager, signInManager, tokenService));
            _tokenService = new Lazy<ITokenService>(() =>
                    new TokenService(userManager, configuration));
            _providerService = new Lazy<IProviderService>(() =>
                    new ProviderService(_contextAccessor, repositoryManager, userManager));
            _adminService = new Lazy<IAdminService>(() =>
                    new AdminService(_contextAccessor, repositoryManager, userManager));
            _patientService = new Lazy<IPatientService>(() =>
                    new PatientService(_contextAccessor, repositoryManager, userManager));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public ITokenService TokenService => _tokenService.Value;
        public IProviderService ProviderService => _providerService.Value;
        public IAdminService AdminService => _adminService.Value;
        public IPatientService PatientService => _patientService.Value;
    }
}
