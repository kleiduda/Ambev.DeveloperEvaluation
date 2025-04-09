using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories
{
    public class ListByCategoryRequestValidator : AbstractValidator<ListByCategoryRequest>
    {
        public ListByCategoryRequestValidator()
        {
            RuleFor(x => x.Category).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

}
