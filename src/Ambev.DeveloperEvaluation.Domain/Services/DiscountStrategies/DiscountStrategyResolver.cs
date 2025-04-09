namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies
{
    public static class DiscountStrategyResolver
    {
        public static IDiscountStrategy Resolve(int quantity)
        {
            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items");

            if (quantity >= 10)
                return new TwentyPercentDiscountStrategy();

            if (quantity >= 4)
                return new TenPercentDiscountStrategy();

            return new NoDiscountStrategy();
        }
    }

}
