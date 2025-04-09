using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken);

        Task<(List<Cart> Items, int TotalItems)> GetPaginatedAsync(
        int page,
        int pageSize,
        string? orderBy,
        CancellationToken cancellationToken);


        /// <summary>
        /// Retrieves a cart by ID
        /// </summary>
        /// <param name="id">Cart ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, otherwise null</returns>
        Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a cart in the database
        /// </summary>
        /// <param name="cart">The cart to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task UpdateAsync(Cart cart, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a cart in the database
        /// </summary>
        /// <param name="id">The cart to delee</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);


    }

}
