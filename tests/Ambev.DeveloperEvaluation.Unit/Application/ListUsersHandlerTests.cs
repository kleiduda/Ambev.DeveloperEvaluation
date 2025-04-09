using Ambev.DeveloperEvaluation.Application.Users.GetUsers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class ListUsersHandlerTests
    {
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly ListUsersHandler _handler;

        public ListUsersHandlerTests()
        {
            _handler = new ListUsersHandler(_userRepository);
        }

        [Fact(DisplayName = "Given valid pagination parameters When listing users Then returns paginated result")]
        public async Task Handle_ValidRequest_ReturnsPaginatedUsers()
        {
            // Given
            var command = new ListUsersCommand(1, 10, "username asc");
            var users = Enumerable.Range(0, 5).Select(_ => UserTestData.GenerateValidUser()).ToList();

            _userRepository.GetPaginatedAsync(1, 10, "username asc", Arg.Any<CancellationToken>())
                .Returns((users, totalItems: 5));

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(5);
            result.TotalItems.Should().Be(5);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);

            await _userRepository.Received(1)
                .GetPaginatedAsync(1, 10, "username asc", Arg.Any<CancellationToken>());
        }
    }

}
