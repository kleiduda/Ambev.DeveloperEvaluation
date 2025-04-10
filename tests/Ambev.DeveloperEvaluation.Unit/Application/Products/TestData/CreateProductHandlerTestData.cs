using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class CreateProductHandlerTestData
    {
        private static readonly Faker<CreateProductCommand> ProductCommandFaker = new Faker<CreateProductCommand>("pt_BR")
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 10000))
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Rating, f => new RatingDto
            {
                Rate = f.Random.Double(0, 5),
                Count = f.Random.Int(0, 999)
            });

        private static readonly Faker<Product> ProductFaker = new Faker<Product>("pt_BR")
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(10, 10000))
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Rating, f => new Rating
            {
                Rate = (double)(decimal)f.Random.Double(0, 5),
                Count = f.Random.Int(0, 999)
            });

        public static CreateProductCommand GenerateValidCommand() => ProductCommandFaker.Generate();
        public static Product GenerateValidProduct() => ProductFaker.Generate();

        public static List<Product> GenerateProductList(int count)
            => ProductFaker.Generate(count);
    }

}
