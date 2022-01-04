using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;

namespace Marketplace.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, long>, IUserRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        //public async Task<ICollection<long>> GetUsersIdOnGroupAsync([Range(1, long.MaxValue)] long groupId)
        //{
        //    var usersId = await _context.Users
        //       .Where(p => p.Groups.Any(y => y.id == groupId))
        //       .Select(p => p.id)
        //       .ToListAsync();

        //    return usersId;
        //}

        //public async Task SubscriptionToGroupsAsync([Range(1, long.MaxValue)] long groupId, [Range(1, long.MaxValue)] long userId)
        //{
        //    var user = await _context.Users
        //        .Include(p => p.Groups)
        //        .FirstOrDefaultAsync(p => p.id == userId);

        //    if (user == null)
        //    {
        //        throw new ArgumentException(nameof(user));
        //    }

        //    user.Groups.Add(
        //        _context.Groups
        //        .FirstOrDefault(p => p.id == groupId)
        //    );

        //}

        //public async Task UnsubscriptionToGroupsAsync([Range(1, long.MaxValue)] long groupId, [Range(1, long.MaxValue)] long userId)
        //{
        //    var user = await _context.Users
        //        .Include(p => p.Groups)
        //        .FirstOrDefaultAsync(p => p.id == userId);

        //    if (user == null)
        //    {
        //        throw new ArgumentException(nameof(user));
        //    }

        //    user.Groups.Remove(
        //        _context.Groups
        //        .FirstOrDefault(p => p.id == groupId)
        //    );

        //}
    }
}