using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    /// <summary>
    /// Handles the paginated retrieval of carts
    /// </summary>
    public class ListCartsHandler : IRequestHandler<ListCartsCommand, ListCartsResult>
    {
        private readonly ICartRepository _repository;

        public ListCartsHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListCartsResult> Handle(ListCartsCommand request, CancellationToken cancellationToken)
        {
            var (items, total) = await _repository.GetPaginatedAsync(
                request.Page,
                request.PageSize,
                request.OrderBy,
                cancellationToken);

            return new ListCartsResult
            {
                Items = items,
                TotalItems = total,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }

}
