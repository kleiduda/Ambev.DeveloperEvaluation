namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleResponse
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
    }

}
