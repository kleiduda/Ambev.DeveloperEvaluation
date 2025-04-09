using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Categories
{
    public class ListByCategoryHandler : IRequestHandler<ListByCategoryCommand, ListByCategoryResult>
    {
        private readonly IProductRepository _repository;

        public ListByCategoryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListByCategoryResult> Handle(ListByCategoryCommand request, CancellationToken cancellationToken)
        {
            var (items, total) = await _repository.GetByCategoryAsync(
                request.Category,
                request.Page,
                request.PageSize,
                request.OrderBy,
                cancellationToken);

            return new ListByCategoryResult
            {
                Items = items,
                TotalItems = total,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }

}
