using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUsers
{
    public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
    {
        public ListUsersRequestValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
