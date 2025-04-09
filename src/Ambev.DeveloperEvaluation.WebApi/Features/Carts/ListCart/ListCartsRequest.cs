namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCart
{
    public class ListCartsRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderBy { get; set; }
    }

}
