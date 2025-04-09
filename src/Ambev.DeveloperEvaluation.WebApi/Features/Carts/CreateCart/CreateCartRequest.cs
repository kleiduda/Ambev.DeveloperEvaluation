namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequest
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<CartProductRequest> Products { get; set; } = [];
    }

    public class CartProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
