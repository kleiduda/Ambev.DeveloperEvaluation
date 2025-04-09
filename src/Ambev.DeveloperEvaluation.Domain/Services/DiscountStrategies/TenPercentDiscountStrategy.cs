namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies
{
    public class TenPercentDiscountStrategy : IDiscountStrategy
    {
        public decimal Apply(decimal price, int quantity)
        {
            return price * quantity * 0.90m;
        }
    }
}
