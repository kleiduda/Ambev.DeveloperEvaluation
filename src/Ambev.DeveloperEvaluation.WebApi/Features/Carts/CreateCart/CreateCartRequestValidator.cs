using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleForEach(x => x.Products).SetValidator(new CartProductValidator());
        }
    }

    public class CartProductValidator : AbstractValidator<CartProductRequest>
    {
        public CartProductValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty();
            RuleFor(p => p.Quantity).GreaterThan(0);
        }
    }

}
