
namespace Marketplace.DTO.Models
{
    public abstract class BaseEntityDto<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
