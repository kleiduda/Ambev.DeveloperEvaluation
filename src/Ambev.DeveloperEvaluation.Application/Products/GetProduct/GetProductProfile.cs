using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            CreateMap<Product, GetProductResult>();
            CreateMap<Rating, RatingDto>();
        }
    }

}
