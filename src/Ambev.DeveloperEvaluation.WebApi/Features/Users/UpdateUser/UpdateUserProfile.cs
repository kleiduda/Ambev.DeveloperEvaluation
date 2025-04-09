using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserCommand>();
            CreateMap<NameRequest, NameDto>();
            CreateMap<AddressRequest, AddressDto>();
            CreateMap<GeolocationRequest, GeolocationDto>();

            CreateMap<UpdateUserResult, UpdateUserResponse>();
            CreateMap<NameDto, NameResponse>();
            CreateMap<AddressDto, AddressResponse>();
            CreateMap<GeolocationDto, GeolocationResponse>();
        }
    }

}
