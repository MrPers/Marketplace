using AutoMapper;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Marketplace.Web.Models;

namespace Marketplace.Web.Infrastructure
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserVM, UserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
