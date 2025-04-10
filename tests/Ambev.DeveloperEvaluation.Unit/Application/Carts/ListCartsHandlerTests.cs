using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class ListCartsHandlerTests
    {
        private readonly ICartRepository _repository = Substitute.For<ICartRepository>();
        private readonly ListCartsHandler _handler;

        public ListCartsHandlerTests()
        {
            _handler = new ListCartsHandler(_repository);
        }

        [Fact(DisplayName = "Given valid pagination When listing carts Then returns paginated result")]
        public async Task Handle_ValidPagination_ReturnsPaginatedCarts()
        {
            //TODO - validar corretamente esse test
            // Arrange
            var command = new ListCartsCommand(1, 10, "date desc");
           

            var carts = CartTestData.GenerateCartList(5);

            _repository.GetPaginatedAsync(1, 5, "date desc", Arg.Any<CancellationToken>())
                .Returns((carts, 10));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(0);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);
        }

    }

}
