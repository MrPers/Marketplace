using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class RoleRepository : BaseRepository<Role, RoleDto, Guid>, IRoleRepository
    {
        public RoleRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}


    }
}
