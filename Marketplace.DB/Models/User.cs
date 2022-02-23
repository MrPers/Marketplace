using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models 
{
    public class User : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public User(string userName) : base(userName)
        {
        }
        public User()
        {
        }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public override string UserName { get; set; }
        [Column(TypeName = "varchar(30)")]
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public override string Email { get; set; }
        [NotMapped]
        public override string PhoneNumber { get; set; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
        public virtual ICollection<UserChoice> UserChoices { get; set; } = new List<UserChoice>();
        public virtual ICollection<UserRoleShop> UserRoleShops { get; set; } = new List<UserRoleShop>();
        public virtual ICollection<CommentProduct> CommentProducts { get; set; } = new List<CommentProduct>();
    }
}
