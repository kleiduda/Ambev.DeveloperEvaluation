using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<NameDto, Name>();
        CreateMap<AddressDto, Address>();
        CreateMap<GeolocationDto, Geolocation>();

        CreateMap<User, CreateUserResult>();
        CreateMap<Name, NameDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Geolocation, GeolocationDto>();
    }
}
