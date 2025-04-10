using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken);
        Task<(List<Sale> Items, int TotalItems)> GetPaginatedAsync(
        int page,
        int pageSize,
        string? orderBy,
        string? clienteNome,
        string? numeroVenda,
        DateTime? minDataVenda,
        DateTime? maxDataVenda,
        decimal? minValorTotal,
        decimal? maxValorTotal,
        bool? cancelada,
        CancellationToken cancellationToken);

        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    }

}
