using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Category).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Image).NotEmpty();

            RuleFor(x => x.Rating).SetValidator(new RatingRequestValidator());
        }
    }

    public class RatingRequestValidator : AbstractValidator<RatingRequest>
    {
        public RatingRequestValidator()
        {
            RuleFor(x => x.Rate).InclusiveBetween(0, 5);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }

}
