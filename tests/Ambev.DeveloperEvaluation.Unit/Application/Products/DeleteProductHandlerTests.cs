using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _handler = new DeleteProductHandler(_repository);
        }

        [Fact(DisplayName = "Given existing product ID When deleting Then returns true")]
        public async Task Handle_ProductExists_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            _repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);

            // Act
            var result = await _handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            await _repository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given non-existent product ID When deleting Then returns false")]
        public async Task Handle_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);

            // Act
            var result = await _handler.Handle(new DeleteProductCommand(productId), CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            await _repository.DidNotReceive().DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }
    }


}
