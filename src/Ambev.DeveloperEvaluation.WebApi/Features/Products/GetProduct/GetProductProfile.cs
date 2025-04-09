using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            // Domain → Result
            CreateMap<Product, GetProductResult>();
            CreateMap<Rating, RatingDto>();

            // Result → Response
            CreateMap<GetProductResult, GetProductResponse>();
            CreateMap<RatingDto, RatingResponse>();
        }
    }


}
