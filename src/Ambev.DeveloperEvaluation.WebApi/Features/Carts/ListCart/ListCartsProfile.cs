using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCart
{
    public class ListCartsProfile : Profile
    {
        public ListCartsProfile()
        {
            CreateMap<ListCartsRequest, ListCartsCommand>();
            CreateMap<Cart, ListCartsResponse>();
            CreateMap<CartProduct, CartProductResponse>();
        }
    }

}
