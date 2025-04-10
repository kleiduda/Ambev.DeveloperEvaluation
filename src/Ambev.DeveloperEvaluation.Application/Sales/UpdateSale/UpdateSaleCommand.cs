using Ambev.DeveloperEvaluation.Application.Sales.Dto;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<SaleItemDto> Items { get; set; } = new();
    }

}
