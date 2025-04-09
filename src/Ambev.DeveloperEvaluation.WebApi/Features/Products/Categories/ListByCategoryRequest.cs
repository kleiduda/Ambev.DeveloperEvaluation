namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories
{
    public class ListByCategoryRequest
    {
        public string Category { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }
    }

}
