using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<NameRequest, NameDto>();
        CreateMap<AddressRequest, AddressDto>();
        CreateMap<GeolocationRequest, GeolocationDto>();

        CreateMap<CreateUserResult, CreateUserResponse>();
         CreateMap<NameDto, NameResponse>();
        CreateMap<AddressDto, AddressResponse>();
        CreateMap<GeolocationDto, GeolocationResponse>();
    }
}
