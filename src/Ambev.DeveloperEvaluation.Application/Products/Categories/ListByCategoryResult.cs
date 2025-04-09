using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.Categories
{
    public class ListByCategoryResult
    {
        public IEnumerable<Product> Items { get; set; } = [];
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }

}
