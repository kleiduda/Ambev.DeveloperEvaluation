﻿using Ambev.DeveloperEvaluation.Application.Products;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData
{
    public static class CreateProductHandlerTestData
    {
        private static readonly Faker<CreateProductCommand> ProductFaker = new Faker<CreateProductCommand>("pt_BR")
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

        public static CreateProductCommand GenerateValidCommand() => ProductFaker.Generate();
    }

}
