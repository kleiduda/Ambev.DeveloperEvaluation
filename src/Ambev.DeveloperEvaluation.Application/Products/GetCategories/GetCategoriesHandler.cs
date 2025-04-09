using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<string>>
    {
        private readonly IProductRepository _repository;

        public GetCategoriesHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllCategoriesAsync(cancellationToken);
        }
    }

}
