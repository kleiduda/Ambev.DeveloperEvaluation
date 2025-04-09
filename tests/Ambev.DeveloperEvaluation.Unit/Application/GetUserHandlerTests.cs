using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetUserHandlerTests
    {
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly GetUserHandler _handler;

        public GetUserHandlerTests()
        {
            _handler = new GetUserHandler(_userRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid user ID When handler is called Then returns mapped result")]
        public async Task Handle_ValidUserId_ReturnsUserResult()
        {
            // Given
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);

            var user = new User
            {
                Id = userId,
                Username = "kleiton",
                Email = "kleitonsfreitas@gmail.com",
                Phone = "11953476593",
                Role = UserRole.Customer,
                Status = UserStatus.Active,
                Name = new Name
                {
                    Firstname = "Kleiton",
                    Lastname = "Freitas"
                },
                Address = new Address
                {
                    City = "Indaiatuba",
                    Street = "Av Ary Barnabe",
                    Number = 123,
                    Zipcode = "13332-550",
                    Geolocation = new Geolocation
                    {
                        Lat = "-23.0882",
                        Long = "-47.2234"
                    }
                }
            };

            var expectedResult = new GetUserResult
            {
                Id = userId,
                Name = new NameDto { Firstname = "Kleiton", Lastname = "Freitas" },
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                Status = user.Status,
                Address = new AddressDto
                {
                    City = "Indaiatuba",
                    Street = "Av Ary Barnabe",
                    Number = 123,
                    Zipcode = "13332-550",
                    Geolocation = new GeolocationDto
                    {
                        Lat = "-23.0882",
                        Long = "-47.2234"
                    }
                }
            };

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
            _mapper.Map<GetUserResult>(user).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.Name.Firstname.Should().Be("Kleiton");
            result.Address.City.Should().Be("Indaiatuba");
        }

        [Fact(DisplayName = "Given invalid user ID When handler is called Then throws")]
        public async Task Handle_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>())
                .Returns((User?)null); 

            // When
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {userId} not found");
        }

    }

}
