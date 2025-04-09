using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.UpdateCart
{
    public class UpdateCartHandlerTests
    {
        [Fact(DisplayName = "Given updated cart with multiple items When handling Then recalculates total with discount strategies")]
        public async Task Handle_ValidRequest_UpdatesTotalValueWithDiscounts()
        {
            // Arrange
            var cartId = Guid.NewGuid();

            var command = new UpdateCartCommand
            {
                Id = cartId,
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProductDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 2 },   // no discount
                    new() { ProductId = Guid.NewGuid(), Quantity = 6 },   // 10%
                    new() { ProductId = Guid.NewGuid(), Quantity = 15 }   // 20%
                }
            };

            var products = new Dictionary<Guid, Product>
            {
                { command.Products[0].ProductId, new Product { Id = command.Products[0].ProductId, Price = 10m } },
                { command.Products[1].ProductId, new Product { Id = command.Products[1].ProductId, Price = 20m } },
                { command.Products[2].ProductId, new Product { Id = command.Products[2].ProductId, Price = 30m } }
            };

            var cartToUpdate = new Cart
            {
                Id = cartId,
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProduct>()
            };

            var repository = Substitute.For<ICartRepository>();
            var productRepo = Substitute.For<IProductRepository>();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCartCommand, Cart>();
                cfg.CreateMap<Cart, UpdateCartResult>();
                cfg.CreateMap<CartProductDto, CartProduct>();
                cfg.CreateMap<CartProduct, CartProductDto>();
            }).CreateMapper();

            repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cartToUpdate);

            foreach (var p in products)
                productRepo.GetByIdAsync(p.Key, Arg.Any<CancellationToken>()).Returns(p.Value);

            repository.UpdateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
                .Returns(Task.CompletedTask)
                .AndDoes(ctx =>
                {
                    var cart = ctx.Arg<Cart>();
                    cartToUpdate.TotalValue = cart.TotalValue;
                });

            var handler = new UpdateCartHandler(repository, mapper, productRepo);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedTotal =
                2 * 10m +          // no discount → 20
                6 * 20m * 0.90m +  // 10% → 108
                15 * 30m * 0.80m;  // 20% → 360

            cartToUpdate.TotalValue.Should().Be(expectedTotal);
            result.Should().NotBeNull();
            result.Id.Should().Be(cartId);
        }

    }
}
