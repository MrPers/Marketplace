using Marketplace.DTO.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public T Id { get; set; }
    }
}
