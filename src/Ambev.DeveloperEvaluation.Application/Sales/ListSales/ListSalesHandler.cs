using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<ListSalesCommand, ListSalesResult>
    {
        private readonly ISaleRepository _repository;

        public ListSalesHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListSalesResult> Handle(ListSalesCommand request, CancellationToken cancellationToken)
        {
            var (items, total) = await _repository.GetPaginatedAsync(
                request.Page,
                request.PageSize,
                request.OrderBy,
                request.ClienteNome,
                request.NumeroVenda,
                request._minDataVenda,
                request._maxDataVenda,
                request._minValorTotal,
                request._maxValorTotal,
                request.Cancelada,
                cancellationToken);

            return new ListSalesResult
            {
                Items = items,
                TotalItems = total,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }

}
