using Ambev.DeveloperEvaluation.Application.Products.Categories;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Categories
{
    public class ListByCategoryProfile : Profile
    {
        public ListByCategoryProfile()
        {
            CreateMap<ListByCategoryRequest, ListByCategoryCommand>();
            CreateMap<Product, ListByCategoryResponse>();
            CreateMap<Rating, RatingResponse>();
        }
    }

}
