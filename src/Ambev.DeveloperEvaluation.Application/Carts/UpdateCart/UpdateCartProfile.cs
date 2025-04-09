using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartCommand, Cart>();
            CreateMap<CartProductDto, CartProduct>();

            CreateMap<Cart, UpdateCartResult>();
            CreateMap<CartProduct, CartProductDto>();
        }
    }

}
