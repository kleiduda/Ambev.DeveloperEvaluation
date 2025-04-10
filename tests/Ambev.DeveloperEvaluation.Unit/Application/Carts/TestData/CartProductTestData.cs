using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.TestData
{
    public static class CartTestData
    {
        private static readonly Faker<CartProduct> CartProductFaker = new Faker<CartProduct>("pt_BR")
            .RuleFor(p => p.ProductId, f => Guid.NewGuid())
            .RuleFor(p => p.Quantity, f => f.Random.Int(1, 10));

        private static readonly Faker<Cart> CartFaker = new Faker<Cart>("pt_BR")
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.UserId, f => Guid.NewGuid())
            .RuleFor(c => c.Date, f => f.Date.Recent())
            .RuleFor(c => c.Products, f => CartProductFaker.Generate(f.Random.Int(1, 5)));

        /// <summary>
        /// Generates a valid CartProduct instance
        /// </summary>
        public static CartProduct GenerateValidCartProduct() => CartProductFaker.Generate();

        /// <summary>
        /// Generates a list of valid CartProduct instances
        /// </summary>
        public static List<CartProduct> GenerateCartProductList(int count) =>
            CartProductFaker.Generate(count);

        /// <summary>
        /// Generates a complete valid Cart with random products
        /// </summary>
        public static Cart GenerateValidCart() => CartFaker.Generate();

        /// <summary>
        /// Generates a list of complete valid Carts
        /// </summary>
        public static List<Cart> GenerateCartList(int count) =>
            CartFaker.Generate(count);
    }


}
