using Marketplace.DB.Models;
using Marketplace.DTO.Models;

namespace Marketplace.Contracts.Repository
{
    public interface IProductRepository : IBaseRepository<Product, ProductDto, long>
    {
        //Task<ICollection<long>> GetUsersIdOnGroupAsync(long groupId);
        //Task SubscriptionToGroupsAsync(long groupId, long userId);
        //Task UnsubscriptionToGroupsAsync(long groupId, long userId);
    }
}
