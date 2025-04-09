using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            // Request → Command
            CreateMap<UpdateProductRequest, UpdateProductCommand>();

            // Command → Domain
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<RatingDto, Rating>();

            // Domain → Result
            CreateMap<Product, UpdateProductResult>();
            CreateMap<Rating, RatingDto>();
        }
    }


}
