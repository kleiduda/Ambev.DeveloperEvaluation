using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, GetUserCommand>()
            .ConstructUsing(id => new GetUserCommand(id));

        // Domain ? Application result
        CreateMap<User, GetUserResult>();
        CreateMap<Name, NameDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<Geolocation, GeolocationDto>();

        // Application result ? WebApi response
        CreateMap<GetUserResult, GetUserResponse>();
        CreateMap<NameDto, NameResponse>();
        CreateMap<AddressDto, AddressResponse>();
        CreateMap<GeolocationDto, GeolocationResponse>();
    }
}
