using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Command for deleting a product by ID
    /// </summary>
    /// <param name="Id">The unique identifier of the product</param>
    public record DeleteProductCommand(Guid Id) : IRequest<bool>;

}
