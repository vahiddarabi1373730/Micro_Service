using AutoMapper;
using Catalog.Application.Commands.Products;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Pagination;

namespace Catalog.Application.Mapper;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<ProductBrand, ProductBrandResponse>().ReverseMap();
        CreateMap<ProductType, ProductTypeResponse>().ReverseMap();
        CreateMap<ProductResponse, Product>().ReverseMap();
        CreateMap<AddProductCommand, Product>().ReverseMap();
        CreateMap<UpdateProductCommand, Product>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>();
    }
}