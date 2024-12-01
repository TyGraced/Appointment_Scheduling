using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.DTOs
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? ProviderName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; } 

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "NIN must be exactly 11 digits.")]
        public string NIN { get; set; } = string.Empty;

        public bool IsPatient { get; set; }
        public bool IsProvider { get; set; }
    }
}
