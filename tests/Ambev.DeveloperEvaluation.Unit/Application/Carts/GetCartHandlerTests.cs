using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class GetCartHandlerTests
    {
        private readonly ICartRepository _repository = Substitute.For<ICartRepository>();
        private readonly IMapper _mapper;
        private readonly GetCartHandler _handler;

        public GetCartHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, GetCartResult>();
                cfg.CreateMap<CartProduct, CartProductDto>();
            });

            _mapper = config.CreateMapper();
            _handler = new GetCartHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given existing cart ID When retrieving Then returns cart result")]
        public async Task Handle_CartExists_ReturnsCartResult()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new Cart
            {
                Id = cartId,
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProduct>
                {
                    new CartProduct { ProductId = Guid.NewGuid(), Quantity = 3 }
                }
            };

            _repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);

            // Act
            var result = await _handler.Handle(new GetCartCommand(cartId), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.UserId.Should().Be(cart.UserId);
            result.Products.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Given non-existent cart ID When retrieving Then returns null")]
        public async Task Handle_CartNotFound_ReturnsNull()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            _repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);

            // Act
            var result = await _handler.Handle(new GetCartCommand(cartId), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }

}
