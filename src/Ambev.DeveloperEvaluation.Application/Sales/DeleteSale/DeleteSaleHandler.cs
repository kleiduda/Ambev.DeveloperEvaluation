using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, bool>
    {
        private readonly ISaleRepository _repository;
        private readonly IDomainEventPublisher _eventPublisher;

        public DeleteSaleHandler(ISaleRepository repository, IDomainEventPublisher eventPublisher)
        {
            _repository = repository;
            _eventPublisher = eventPublisher;
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

            var saleCancelledEvent = new SaleCancelledEvent(sale.Id, "Cancelled by user request");
            await _eventPublisher.PublishAsync(saleCancelledEvent, cancellationToken);

            return true;
        }
    }


}
