using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;
public record ListUsersCommand(int Page, int PageSize, string? OrderBy) : IRequest<ListUsersResult>;