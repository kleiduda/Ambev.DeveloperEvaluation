namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesResponse
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; } = string.Empty;
        public string ClienteNome { get; set; } = string.Empty;
        public string FilialNome { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Cancelada { get; set; }
    }

}
