using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult?>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProductResult?> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return product is null ? null : _mapper.Map<GetProductResult>(product);
        }
    }

}
