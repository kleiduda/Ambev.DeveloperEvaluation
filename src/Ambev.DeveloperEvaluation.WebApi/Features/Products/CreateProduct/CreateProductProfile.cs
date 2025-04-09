using Ambev.DeveloperEvaluation.Application.Products;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<RatingRequest, RatingDto>();

            CreateMap<CreateProductResult, CreateProductResponse>();
            CreateMap<RatingDto, RatingResponse>();
        }
    }

}
