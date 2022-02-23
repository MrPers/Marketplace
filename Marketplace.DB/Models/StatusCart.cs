using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class StatusUserChoice : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(10)")]
        public string Name { get; set; }
    }
}
