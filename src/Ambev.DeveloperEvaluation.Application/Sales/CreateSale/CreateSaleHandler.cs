using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = _mapper.Map<Sale>(command);

            sale.TotalAmount = sale.Items.Sum(i => (i.UnitPrice * i.Quantity) - i.Discount);
            sale.IsCancelled = false;

            var created = await _repository.CreateAsync(sale, cancellationToken);

            return new CreateSaleResult
            {
                Id = created.Id,
                SaleNumber = created.SaleNumber,
                TotalAmount = created.TotalAmount
            };
        }
    }

}
