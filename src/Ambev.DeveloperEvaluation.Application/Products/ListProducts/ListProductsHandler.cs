using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsHandler : IRequestHandler<ListProductsCommand, ListProductsResult>
    {
        private readonly IProductRepository _repository;

        public ListProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListProductsResult> Handle(ListProductsCommand request, CancellationToken cancellationToken)
        {
            var (items, total) = await _repository.GetPaginatedAsync(
                request.Page,
                request.PageSize,
                request.OrderBy,
                cancellationToken);

            return new ListProductsResult
            {
                Items = items,
                TotalItems = total,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }

}
