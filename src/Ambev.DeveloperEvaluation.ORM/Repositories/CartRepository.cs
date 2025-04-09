using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task<(List<Cart> Items, int TotalItems)> GetPaginatedAsync(
        int page,
        int pageSize,
        string? orderBy,
        CancellationToken cancellationToken)
        {
            var query = _context.Carts.Include(c => c.Products).AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                try
                {
                    query = query.OrderBy(orderBy); 
                }
                catch
                {
                    query = query.OrderBy(c => c.Id);
                }
            }
            else
            {
                query = query.OrderBy(c => c.Id);
            }

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalItems);
        }


        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await _context.Carts.FindAsync(new object[] { id }, cancellationToken);

            if (cart is not null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }



    }

}
