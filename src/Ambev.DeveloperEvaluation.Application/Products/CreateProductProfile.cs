using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<RatingDto, Rating>();

            CreateMap<Product, CreateProductResult>();
            CreateMap<Rating, RatingDto>();
        }
    }

}
