using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleForEach(x => x.Products).SetValidator(new CartProductValidator());
        }
    }

}
