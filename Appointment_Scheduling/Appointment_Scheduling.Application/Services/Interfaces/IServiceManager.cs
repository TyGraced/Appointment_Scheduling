namespace Appointment_Scheduling.Application.Services.Interfaces
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        ITokenService TokenService { get; }
    }
}
