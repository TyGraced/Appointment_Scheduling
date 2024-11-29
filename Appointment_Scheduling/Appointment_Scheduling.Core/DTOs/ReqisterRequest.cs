﻿using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduling.Core.DTOs
{
    public class ReqisterRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public bool IsPatient { get; set; }
        public bool IsProvider { get; set; }
    }
}
