﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public string LastName { get; set; }
        
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
