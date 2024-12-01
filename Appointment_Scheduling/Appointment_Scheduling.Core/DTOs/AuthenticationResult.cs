namespace Appointment_Scheduling.Core.DTOs
{
    public class AuthenticationResult
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
    }
}
