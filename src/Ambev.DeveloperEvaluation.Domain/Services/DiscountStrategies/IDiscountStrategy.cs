namespace Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies
{
    public interface IDiscountStrategy
    {
        decimal Apply(decimal price, int quantity);
    }

}
