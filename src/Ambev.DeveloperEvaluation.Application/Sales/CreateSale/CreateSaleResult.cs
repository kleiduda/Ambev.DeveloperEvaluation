namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
    }

}
