using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISaleRepository _repository;

        public DeleteSaleHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                return false;

            if (sale.IsCancelled)
                return true; // já cancelada, idempotente

            sale.IsCancelled = true;
            await _repository.UpdateAsync(sale, cancellationToken);
            return true;
        }
    }


}
