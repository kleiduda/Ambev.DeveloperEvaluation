namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies
{
    public class TwentyPercentDiscountStrategy : IDiscountStrategy
    {
        public decimal Apply(decimal price, int quantity)
        {
            return price * quantity * 0.80m;
        }
    }
}
