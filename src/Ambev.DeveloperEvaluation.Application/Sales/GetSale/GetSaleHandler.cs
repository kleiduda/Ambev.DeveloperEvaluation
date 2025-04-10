using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult?>
    {
        private readonly ISaleRepository _repository;

        public GetSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetSaleResult?> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                return null;

            return new GetSaleResult { Sale = sale };
        }
    }

}
