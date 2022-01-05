using Marketplace.DTO.Models;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.DB.Models
{
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
