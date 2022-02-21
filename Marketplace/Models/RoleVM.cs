using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class RoleVM
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}