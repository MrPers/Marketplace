using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Models
{
    public class UserVM
    {
        public long Id { get; set; }
        [Required]
        [MinLength(40)]
        public string UserName { get; set; }
        [MinLength(40)]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
