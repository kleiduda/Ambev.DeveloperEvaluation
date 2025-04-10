namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class ListSalesRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }

        public string? ClienteNome { get; set; }
        public string? NumeroVenda { get; set; }

        public DateTime? _minDataVenda { get; set; }
        public DateTime? _maxDataVenda { get; set; }

        public decimal? _minValorTotal { get; set; }
        public decimal? _maxValorTotal { get; set; }

        public bool? Cancelada { get; set; }
    }

}
