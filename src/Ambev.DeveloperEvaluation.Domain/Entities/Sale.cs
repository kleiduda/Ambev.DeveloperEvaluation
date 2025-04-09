using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string NumeroVenda { get; set; } = string.Empty;
        public DateTime DataVenda { get; set; }

        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; } = string.Empty;

        public Guid FilialId { get; set; }
        public string FilialNome { get; set; } = string.Empty;

        public decimal ValorTotal { get; set; }
        public bool Cancelada { get; set; }

        public List<SaleItem> Itens { get; set; } = new();
    }

}
