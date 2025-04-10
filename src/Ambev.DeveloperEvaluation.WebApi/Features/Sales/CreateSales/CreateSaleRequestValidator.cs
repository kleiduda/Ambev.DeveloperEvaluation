using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.NumeroVenda).NotEmpty();
            RuleFor(x => x.ClienteNome).NotEmpty();
            RuleFor(x => x.FilialNome).NotEmpty();
            RuleForEach(x => x.Itens).SetValidator(new SaleItemValidator());
        }
    }

    public class SaleItemValidator : AbstractValidator<SaleItemRequest>
    {
        public SaleItemValidator()
        {
            RuleFor(x => x.ProdutoId).NotEmpty();
            RuleFor(x => x.ProdutoNome).NotEmpty();
            RuleFor(x => x.Quantidade).GreaterThan(0);
            RuleFor(x => x.PrecoUnitario).GreaterThan(0);
            RuleFor(x => x.Desconto).GreaterThanOrEqualTo(0);
        }
    }

}
