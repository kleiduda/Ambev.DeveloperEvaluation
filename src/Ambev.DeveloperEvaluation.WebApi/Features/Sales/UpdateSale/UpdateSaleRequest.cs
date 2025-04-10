using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid Id { get; set; }

        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<SaleItemRequest> Items { get; set; } = new();
    }


}
