﻿using AutoMapper;
using Marketplace.Models;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;

namespace Marketplace.Infrastructure
{
    public class Mapper : Profile
    {
        public Mapper()
        {
                //.ForMember(dest => dest.ProductGroup.Name, opt => opt.MapFrom(c => c.ProductGroupName));



            CreateMap<Cart, CartDto>().ReverseMap();
            //CreateMap<CartVM, CartDto>().ReverseMap();

            CreateMap<CommentProductVM, CommentProductDto>().ReverseMap();
            CreateMap<CommentProduct, CommentProductDto>().ReverseMap();
            
            //CreateMap<ClaimVM, ClaimDto>().ReverseMap();
            CreateMap<Claim, ClaimDto>().ReverseMap();

            CreateMap<PriceVM, PriceDto>().ReverseMap();
            CreateMap<Price, PriceDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, FullProductVM> ().ReverseMap();
            CreateMap<ProductDto, BriefProductVM>();
            //CreateMap<BriefProductDto, BriefProductVM>();

            CreateMap<ProductGroupVM, ProductGroupDto>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();

            CreateMap<RoleVM, RoleDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<ShopVM, ShopDto>().ReverseMap();
            CreateMap<Shop, ShopDto>().ReverseMap();

            CreateMap<StatusCart, StatusCartDto>().ReverseMap();
            //CreateMap<StatusCartVM, StatusCartDto>().ReverseMap();

            CreateMap<UserVM, UserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}