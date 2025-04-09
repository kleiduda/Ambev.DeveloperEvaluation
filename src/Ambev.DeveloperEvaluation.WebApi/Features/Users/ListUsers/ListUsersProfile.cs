using Ambev.DeveloperEvaluation.Application.Users.GetUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUsers
{
    public class ListUsersProfile : Profile
    {
        public ListUsersProfile()
        {
            // Domain → Response DTO (WebApi)
            CreateMap<User, ListUsersResponse>();
            CreateMap<Name, NameResponse>();
            CreateMap<Address, AddressResponse>();
            CreateMap<Geolocation, GeolocationResponse>();

            // Opcional: Request da WebApi → Command da Application
            CreateMap<ListUsersRequest, GetUsersCommand>();
        }
    }
}
