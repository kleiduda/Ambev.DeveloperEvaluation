using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a cart by ID
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, otherwise null</returns>
        Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken);


    }

}
