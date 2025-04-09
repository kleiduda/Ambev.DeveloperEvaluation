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


    }

}
