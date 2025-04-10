using Ambev.DeveloperEvaluation.Application.Users.GetUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
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

        [Fact(DisplayName = "Given valid parameters When no users exist Then returns empty result")]
        public async Task Handle_EmptyResult_ReturnsEmptyList()
        {
            // Given
            var command = new ListUsersCommand(1, 10, "username asc");

            _userRepository.GetPaginatedAsync(1, 10, "username asc", Arg.Any<CancellationToken>())
                .Returns((new List<User>(), totalItems: 0));

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Items.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);

            await _userRepository.Received(1)
                .GetPaginatedAsync(1, 10, "username asc", Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given second page When listing users Then returns correct page data")]
        public async Task Handle_Pagination_ReturnsCorrectPage()
        {
            // Given
            var command = new ListUsersCommand(2, 10, "username asc");
            var users = Enumerable.Range(0, 3).Select(_ => UserTestData.GenerateValidUser()).ToList();

            _userRepository.GetPaginatedAsync(2, 10, "username asc", Arg.Any<CancellationToken>())
                .Returns((users, totalItems: 13));

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(3);
            result.TotalItems.Should().Be(13);
            result.CurrentPage.Should().Be(2);
            result.PageSize.Should().Be(10);

            await _userRepository.Received(1)
                .GetPaginatedAsync(2, 10, "username asc", Arg.Any<CancellationToken>());
        }


    }

}
