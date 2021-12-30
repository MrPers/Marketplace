using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    //public class Claim : System.Security.Claims.Claim, IBaseEntity<long>
    public class ClaimDto : BaseEntityDto<long>
    {
        public string Name { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
    }
}
