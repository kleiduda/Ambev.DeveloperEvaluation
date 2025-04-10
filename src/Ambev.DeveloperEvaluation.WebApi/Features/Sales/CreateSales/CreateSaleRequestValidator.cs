using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new SaleItemValidator());
        }
    }

    public class SaleItemValidator : AbstractValidator<SaleItemRequest>
    {
        public SaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
        }
    }

}
