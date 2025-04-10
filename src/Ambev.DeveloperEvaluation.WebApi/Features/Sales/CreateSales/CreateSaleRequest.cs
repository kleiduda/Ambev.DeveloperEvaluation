namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales
{
    public class CreateSaleRequest
    {
        public string NumeroVenda { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }

        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;

        public Guid FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;

        public List<SaleItemRequest> Itens { get; set; } = new();
    }

    public class SaleItemRequest
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
    }

}
