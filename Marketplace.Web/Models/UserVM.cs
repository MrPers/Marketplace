using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Models
{
    public class UserVM
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
