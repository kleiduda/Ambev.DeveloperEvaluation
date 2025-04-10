using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesCommand : IRequest<ListSalesResult>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
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
