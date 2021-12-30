using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class StatusCart : BaseEntity<long>
    {
        [Column(TypeName = "varchar(10)")]
        public string Name { get; set; }
    }
}
