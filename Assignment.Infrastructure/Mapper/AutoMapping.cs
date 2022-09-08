using AutoMapper;
using Assignment.Data.Entities;
using Assignment.Infrastructure.DTOs;
using Models.WebApiViewModels;
using System;

namespace Assignment.Infrastructure.Mapper
{
    /// <summary> Automapping Class</summary>
    public class AutoMapping : Profile
    {
        /// <summary> Automapping CTOR</summary>
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto,Product>()
                .ForMember(a => a.ProductCategoryId, a => a.MapFrom(u => u.CategoryId)).ReverseMap();

            CreateMap<PatchProductDto, Product>()
               .ForMember(a => a.ProductCategoryId, a => a.MapFrom(u => u.CategoryId)).ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<RegisterDto,ApplicationUser>()
                .ForMember(a => a.UserName, a => a.MapFrom(u => u.Email))
                .ForMember(a => a.Email, a => a.MapFrom(u => u.Email))
                .ForMember(a => a.CreateDate, a => a.MapFrom(u => DateTime.Now))
                .ReverseMap();
            CreateMap<ApplicationUser, LoginResponse>().ReverseMap();
        }
    }
}
