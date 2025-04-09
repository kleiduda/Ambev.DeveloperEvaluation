using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;
public class ListUsersResult
{
    public IEnumerable<User> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
