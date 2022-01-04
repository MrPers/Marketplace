﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class UserDto : IdentityUser<long>, IBaseEntity<long>
    {
        public override string UserName { get; set; }
        public override string Email { get; set; }
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; }
        public virtual ICollection<UserShopDto> UserShops { get; set; }
        public virtual ICollection<CartDto> Carts { get; set; }
        public virtual ICollection<CommentProductDto> CommentProducts { get; set; }
    }
}
