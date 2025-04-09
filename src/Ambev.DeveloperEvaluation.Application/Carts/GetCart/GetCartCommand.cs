using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Command for retrieving a cart by ID
    /// </summary>
    /// <param name="Id">The cart's unique identifier</param>
    public record GetCartCommand(Guid Id) : IRequest<GetCartResult?>;


}
