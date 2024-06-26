﻿using System.ComponentModel.DataAnnotations;

namespace IdentityApp.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "First Name must be at least {2} characters, and maximum {1} characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Last Name must be at least {2} characters, and maximum {1} characters.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be at least {2} characters, and maximum {1} characters.")]
        public string Password { get; set; }
    }
}
