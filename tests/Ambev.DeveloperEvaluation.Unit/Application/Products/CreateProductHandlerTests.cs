using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _handler = new CreateProductHandler(_repository, _mapper);
        }

        [Fact(DisplayName = "Given valid command When creating product Then returns result")]
        public async Task Handle_ValidCommand_ReturnsResult()
        {
            // Arrange
            var command = CreateProductHandlerTestData.GenerateValidCommand();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Price = command.Price,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Rating = new Rating
                {
                    Rate = command.Rating.Rate,
                    Count = command.Rating.Count
                }
            };

            var expectedResult = new CreateProductResult
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Image = product.Image,
                Rating = new RatingDto
                {
                    Rate = product.Rating.Rate,
                    Count = product.Rating.Count
                }
            };

            _mapper.Map<Product>(command).Returns(product);
            _repository.CreateAsync(product, Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<CreateProductResult>(product).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Title.Should().Be(product.Title);
            result.Price.Should().Be(product.Price);
            result.Description.Should().Be(product.Description);
            result.Rating.Rate.Should().Be(product.Rating.Rate);
        }

    }

}
