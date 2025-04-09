namespace Ambev.DeveloperEvaluation.Application.Users.GetUsers;
public class ListUsersRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
}
