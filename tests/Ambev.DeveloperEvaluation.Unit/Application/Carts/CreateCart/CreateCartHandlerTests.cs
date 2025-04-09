using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.CreateCart
{
    public class CreateCartHandlerTests
    {
        [Fact(DisplayName = "Given cart with multiple items When creating Then calcultes total with discount rules")]
        public async Task Handle_ValidRequest_CalculatesTotalValueWithDiscounts()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProductDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 3 },   // no discount
                    new() { ProductId = Guid.NewGuid(), Quantity = 5 },   // 10% discount
                    new() { ProductId = Guid.NewGuid(), Quantity = 12 }   // 20% discount
                }
            };

            var products = new Dictionary<Guid, Product>
            {
                { command.Products[0].ProductId, new Product { Id = command.Products[0].ProductId, Price = 10m } },
                { command.Products[1].ProductId, new Product { Id = command.Products[1].ProductId, Price = 20m } },
                { command.Products[2].ProductId, new Product { Id = command.Products[2].ProductId, Price = 30m } }
            };

            var fakeCart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Date = command.Date,
                Products = command.Products.Select(p => new CartProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };

            var repository = Substitute.For<ICartRepository>();
            var productRepo = Substitute.For<IProductRepository>();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCartCommand, Cart>();
                cfg.CreateMap<Cart, CreateCartResult>();
                cfg.CreateMap<CartProductDto, CartProduct>();
                cfg.CreateMap<CartProduct, CartProductDto>();
            }).CreateMapper();

            foreach (var p in products)
                productRepo.GetByIdAsync(p.Key, Arg.Any<CancellationToken>()).Returns(p.Value);

            repository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
                .Returns(callInfo =>
                {
                    var cart = callInfo.Arg<Cart>();
                    fakeCart.TotalValue = cart.TotalValue;
                    return fakeCart;
                });

            var handler = new CreateCartHandler(repository, productRepo, mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedTotal =
                3 * 10m +        // no discount → 30
                5 * 20m * 0.90m + // 10% → 90
                12 * 30m * 0.80m; // 20% → 288 ll

            result.Should().NotBeNull();
            result.Id.Should().Be(fakeCart.Id);
            fakeCart.TotalValue.Should().Be(expectedTotal);
        }

        [Fact(DisplayName = "Given product quantity above 20 When creating cart Then throws DomainException")]
        public async Task Handle_QuantityAbove20_ThrowsDomainException()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Products = new List<CartProductDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 25 }
                }
            };

            var product = new Product
            {
                Id = command.Products[0].ProductId,
                Price = 10m
            };

            var repository = Substitute.For<ICartRepository>();
            var productRepo = Substitute.For<IProductRepository>();
            productRepo.GetByIdAsync(product.Id, Arg.Any<CancellationToken>()).Returns(product);

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCartCommand, Cart>();
                cfg.CreateMap<Cart, CreateCartResult>();
                cfg.CreateMap<CartProductDto, CartProduct>();
                cfg.CreateMap<CartProduct, CartProductDto>();
            }).CreateMapper();

            var handler = new CreateCartHandler(repository, productRepo, mapper);

            // Act
            var act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should()
                .ThrowAsync<DomainException>()
                .WithMessage("Cannot sell more than 20 identical items*");
        }


    }
}
