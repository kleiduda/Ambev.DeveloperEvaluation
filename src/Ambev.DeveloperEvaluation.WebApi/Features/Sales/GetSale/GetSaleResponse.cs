namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string NumeroVenda { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }

        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;

        public Guid FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;

        public decimal ValorTotal { get; set; }
        public bool Cancelada { get; set; }

        public List<GetSaleItemResponse> Itens { get; set; } = new();
    }

    public class GetSaleItemResponse
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotalItem => (PrecoUnitario * Quantidade) - Desconto;
    }

}
