using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);
        Task<(List<Product> Items, int TotalItems)> GetPaginatedAsync(
        int page,
        int pageSize,
        string? orderBy,
        CancellationToken cancellationToken);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a product by its ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Get a list of categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        Task<List<string>> GetAllCategoriesAsync(CancellationToken cancellationToken);



    }

}
