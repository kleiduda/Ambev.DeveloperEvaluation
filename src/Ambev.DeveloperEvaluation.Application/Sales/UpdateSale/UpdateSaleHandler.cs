using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDomainEventPublisher _eventPublisher;

        public UpdateSaleHandler(ISaleRepository repository, IMapper mapper, IDomainEventPublisher eventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }


        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null || sale.IsCancelled)
                return false;

            sale.SaleNumber = request.SaleNumber;
            sale.SaleDate = request.SaleDate;
            sale.CustomerId = request.CustomerId;
            sale.CustomerName = request.CustomerName;
            sale.BranchId = request.BranchId;
            sale.BranchName = request.BranchName;
            sale.Items = _mapper.Map<List<SaleItem>>(request.Items);
            sale.TotalAmount = sale.Items.Sum(i => (i.UnitPrice * i.Quantity) - i.Discount);

            await _repository.UpdateAsync(sale, cancellationToken);

            var saleModifiedEvent = new SaleModifiedEvent(sale.Id, DateTime.UtcNow);
            await _eventPublisher.PublishAsync(saleModifiedEvent, cancellationToken);

            return true;
        }
    }

}
