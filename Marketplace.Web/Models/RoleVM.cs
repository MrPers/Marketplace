using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Marketplace.Web.Models
{
    public class RoleVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}