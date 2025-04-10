using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<(List<Sale> Items, int TotalItems)> GetPaginatedAsync(
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
        CancellationToken cancellationToken)
        {
            var query = _context.Sales.AsQueryable();

            if (!string.IsNullOrWhiteSpace(clienteNome))
            {
                if (clienteNome.StartsWith("*"))
                    query = query.Where(x => x.ClienteNome.EndsWith(clienteNome.TrimStart('*')));
                else if (clienteNome.EndsWith("*"))
                    query = query.Where(x => x.ClienteNome.StartsWith(clienteNome.TrimEnd('*')));
                else
                    query = query.Where(x => x.ClienteNome.Contains(clienteNome));
            }

            if (!string.IsNullOrWhiteSpace(numeroVenda))
            {
                if (numeroVenda.StartsWith("*"))
                    query = query.Where(x => x.NumeroVenda.EndsWith(numeroVenda.TrimStart('*')));
                else if (numeroVenda.EndsWith("*"))
                    query = query.Where(x => x.NumeroVenda.StartsWith(numeroVenda.TrimEnd('*')));
                else
                    query = query.Where(x => x.NumeroVenda.Contains(numeroVenda));
            }

            if (minDataVenda.HasValue)
                query = query.Where(x => x.DataVenda >= minDataVenda.Value);
            if (maxDataVenda.HasValue)
                query = query.Where(x => x.DataVenda <= maxDataVenda.Value);

            if (minValorTotal.HasValue)
                query = query.Where(x => x.ValorTotal >= minValorTotal.Value);
            if (maxValorTotal.HasValue)
                query = query.Where(x => x.ValorTotal <= maxValorTotal.Value);

            if (cancelada.HasValue)
                query = query.Where(x => x.Cancelada == cancelada.Value);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                try
                {
                    query = query.OrderBy(orderBy); // System.Linq.Dynamic.Core
                }
                catch
                {
                    query = query.OrderBy(x => x.DataVenda);
                }
            }
            else
            {
                query = query.OrderBy(x => x.DataVenda);
            }

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalItems);
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Itens)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales
                .Include(s => s.Itens)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (sale is not null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }



    }

}
