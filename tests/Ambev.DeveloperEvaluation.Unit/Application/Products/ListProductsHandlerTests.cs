using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class ListProductsHandlerTests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly ListProductsHandler _handler;

        public ListProductsHandlerTests()
        {
            _handler = new ListProductsHandler(_repository);
        }

        [Fact(DisplayName = "Given valid pagination When listing products Then returns paginated result")]
        public async Task Handle_ValidPagination_ReturnsPaginatedProducts()
        {
            // Arrange
            var command = new ListProductsCommand(2, 5, "title asc");

            var products = Enumerable.Range(0, 5)
            .Select(_ => CreateProductHandlerTestData.GenerateValidProduct())
            .ToList();

            _repository.GetPaginatedAsync(2, 5, "title asc", Arg.Any<CancellationToken>())
                .Returns((products, total: 15));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(5);
            result.TotalItems.Should().Be(15);
            result.CurrentPage.Should().Be(2);
            result.PageSize.Should().Be(5);

            await _repository.Received(1)
                .GetPaginatedAsync(2, 5, "title asc", Arg.Any<CancellationToken>());
        }
    }

}
