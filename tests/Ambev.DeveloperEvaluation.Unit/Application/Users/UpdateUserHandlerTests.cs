using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class UpdateUserHandlerTests
    {
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IPasswordHasher _passwordHasher = Substitute.For<IPasswordHasher>();
        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTests()
        {
            _handler = new UpdateUserHandler(_userRepository, _mapper, _passwordHasher);
        }

        [Fact(DisplayName = "Given valid update command When user exists Then updates and returns result")]
        public async Task Handle_ValidCommand_UpdatesUserSuccessfully()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateValidCommand();
            var user = UserTestData.GenerateValidUser();
            var hashedPassword = "hashedPassword";

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(user);
            _passwordHasher.HashPassword(command.Password).Returns(hashedPassword);
            _userRepository.UpdateAsync(user, Arg.Any<CancellationToken>()).Returns(user);

            var expectedResult = new UpdateUserResult
            {
                Id = user.Id,
                Username = command.Username,
                Email = command.Email,
                Phone = command.Phone,
                Status = command.Status,
                Role = command.Role,
                Name = command.Name,
                Address = command.Address
            };

            _mapper.Map<Name>(command.Name).Returns(user.Name);
            _mapper.Map<Address>(command.Address).Returns(user.Address);
            _mapper.Map<UpdateUserResult>(user).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            await _userRepository.Received(1).UpdateAsync(user, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid user ID When updating user Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = UpdateUserHandlerTestData.GenerateValidCommand();

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((User?)null);

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {command.Id} not found");
        }

    }

}
