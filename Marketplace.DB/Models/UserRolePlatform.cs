using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class UserRolePlatform : IdentityUserRole<Guid>
    {
    }
}
