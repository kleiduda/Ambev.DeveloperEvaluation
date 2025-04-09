using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;
public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        CreateMap<ListUsersRequest, ListUsersCommand>();
        CreateMap<User, ListUsersResult>();
    }
}
