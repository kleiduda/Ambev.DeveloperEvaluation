using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartRequest, CreateCartCommand>();
            CreateMap<CartProductRequest, CartProductDto>();

            CreateMap<CreateCartResult, CreateCartResponse>();
            CreateMap<CartProductDto, CartProductResponse>();
        }
    }

}
