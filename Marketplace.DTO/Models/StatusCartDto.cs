using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    public class StatusCartDto : BaseEntityDto<long>
    {
        public string Name { get; set; }
    }
}
