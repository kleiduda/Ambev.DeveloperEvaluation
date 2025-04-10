using Ambev.DeveloperEvaluation.Application.Sales.Dto;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public string NumeroVenda { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }

        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;

        public Guid FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;

        public List<SaleItemDto> Itens { get; set; } = new();
    }

}
