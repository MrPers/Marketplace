
namespace Marketplace.DTO.Models
{
    public class BaseEntityDto<T> : IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
