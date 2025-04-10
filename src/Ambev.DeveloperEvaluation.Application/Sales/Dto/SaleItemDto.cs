namespace Ambev.DeveloperEvaluation.Application.Sales.Dto
{
    public class SaleItemDto
    {
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
    }

}
