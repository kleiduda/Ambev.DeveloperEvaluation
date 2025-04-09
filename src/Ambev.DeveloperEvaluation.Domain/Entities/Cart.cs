using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProduct> Products { get; set; } = new();
    }

    public class CartProduct
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
