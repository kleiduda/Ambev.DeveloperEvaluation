
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;
public class ListUsersHandler : IRequestHandler<ListUsersCommand, ListUsersResult>
{
    private readonly IUserRepository _userRepository;

    public ListUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ListUsersResult> Handle(ListUsersCommand request, CancellationToken cancellationToken)
    {
        var paginatedResult = await _userRepository.GetPaginatedAsync(
            page: request.Page,
            pageSize: request.PageSize,
            orderBy: request.OrderBy,
            cancellationToken: cancellationToken);

        return new ListUsersResult
        {
            Items = paginatedResult.Items,
            TotalItems = paginatedResult.TotalItems,
            PageSize = request.PageSize,
            CurrentPage = request.Page
        };
    }
}
