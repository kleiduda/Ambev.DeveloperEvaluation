using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductRequest> Products { get; set; } = new();
    }

}
