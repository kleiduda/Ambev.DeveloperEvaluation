using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IMapper _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProductCommand, Product>();
                cfg.CreateMap<RatingDto, Rating>();
                cfg.CreateMap<Product, UpdateProductResult>();
                cfg.CreateMap<Rating, RatingDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateProductHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given existing product When updating Then returns updated result")]
        public async Task Handle_ExistingProduct_ReturnsUpdatedResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var command = new UpdateProductCommand
            {
                Id = productId,
                Title = "Updated Product",
                Price = 150.50M,
                Description = "Updated Desc",
                Category = "Electronics",
                Image = "image.jpg",
                Rating = new RatingDto { Rate = 4.5, Count = 200 }
            };

            var existingProduct = CreateProductHandlerTestData.GenerateValidProduct();
            existingProduct.Id = productId;

            _repository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
                .Returns(existingProduct);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Title.Should().Be(command.Title);
            result.Price.Should().Be(command.Price);

            await _repository.Received(1).UpdateAsync(
                Arg.Is<Product>(p =>
                    p.Title == command.Title &&
                    p.Price == command.Price),
                Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent product When updating Then throws KeyNotFoundException")]
        public async Task Handle_ProductNotFound_ThrowsException()
        {
            // Arrange
            var command = CreateProductHandlerTestData.GenerateValidCommand();
            var updateCommand = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Price = command.Price,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Rating = command.Rating
            };

            _repository.GetByIdAsync(updateCommand.Id, Arg.Any<CancellationToken>())
                .Returns((Product?)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Product with ID {updateCommand.Id} not found");
        }
    }


}
