using Ambev.DeveloperEvaluation.Application.Users.CreateUser.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UpdateUserResult>();

            CreateMap<NameDto, Name>();
            CreateMap<AddressDto, Address>();
            CreateMap<GeolocationDto, Geolocation>();

            CreateMap<Name, NameDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<Geolocation, GeolocationDto>();
        }
    }

}
