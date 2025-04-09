using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Command to delete a cart
    /// </summary>
    /// <param name="Id">The ID of the cart</param>
    public record DeleteCartCommand(Guid Id) : IRequest;

}
