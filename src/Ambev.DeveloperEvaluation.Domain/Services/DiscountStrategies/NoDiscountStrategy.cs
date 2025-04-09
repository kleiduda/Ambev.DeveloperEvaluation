namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal Apply(decimal price, int quantity)
        {
            return price * quantity;
        }
    }
}
