using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    //public class Claim : System.Security.Claims.Claim, IBaseEntity<Guid>
    public class ClaimDto : BaseEntityDto<int>
    {
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

    }
}
