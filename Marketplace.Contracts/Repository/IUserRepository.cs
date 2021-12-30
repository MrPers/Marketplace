using Marketplace.DB.Models;
using Marketplace.DTO.Models;

namespace Marketplace.Contracts.Repository
{
    public interface IUserRepository : IBaseRepository<User, UserDto, long>
    {

    }
}
