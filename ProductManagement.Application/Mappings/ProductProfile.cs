using AutoMapper;
using ProductManagement.Application.DTOs.Requests;
using ProductManagement.Application.DTOs.Responses;
using ProductManagement.Application.Entities;

namespace ProductManagement.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>();

            CreateMap<CreateProductRequest, Product>();

            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
