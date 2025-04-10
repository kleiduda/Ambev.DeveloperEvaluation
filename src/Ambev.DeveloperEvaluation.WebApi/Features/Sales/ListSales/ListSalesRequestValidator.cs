using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
    {
        public ListSalesRequestValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }

}
