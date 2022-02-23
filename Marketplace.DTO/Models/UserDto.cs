using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.DTO.Models
{
    public class UserDto : BaseEntityDto<Guid>
    {
        [Required]
        //[MinLength(40)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserChoiceDto> UserChoices { get; set; }
        public virtual ICollection<CommentProductDto> CommentProducts { get; set; }
    }
}
