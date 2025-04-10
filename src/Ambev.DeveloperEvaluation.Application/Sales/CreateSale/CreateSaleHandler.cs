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

            sale.ValorTotal = sale.Itens.Sum(i => (i.PrecoUnitario * i.Quantidade) - i.Desconto);
            sale.Cancelada = false;

            var created = await _repository.CreateAsync(sale, cancellationToken);

            return new CreateSaleResult
            {
                Id = created.Id,
                NumeroVenda = created.NumeroVenda,
                ValorTotal = created.ValorTotal
            };
        }
    }

}
