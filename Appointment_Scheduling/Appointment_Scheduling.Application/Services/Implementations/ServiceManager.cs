using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.Models;
using Appointment_Scheduling.Core.Settings;
using Appointment_Scheduling.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Appointment_Scheduling.Application.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ITokenService> _tokenService;
        public ServiceManager(IRepositoryManager repositoryManager,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> configuration, ITokenService tokenService)
        {
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                    new AuthenticationService(userManager, signInManager, tokenService));
            _tokenService = new Lazy<ITokenService>(() =>
                    new TokenService(userManager, configuration));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public ITokenService TokenService => _tokenService.Value;
    }
}
