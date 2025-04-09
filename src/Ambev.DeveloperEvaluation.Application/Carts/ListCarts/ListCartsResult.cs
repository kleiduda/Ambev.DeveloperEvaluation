using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsResult
    {
        public IEnumerable<Cart> Items { get; set; } = [];
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

}
