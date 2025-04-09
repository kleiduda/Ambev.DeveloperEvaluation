using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Handles the deletion of a cart
    /// </summary>
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
    {
        private readonly ICartRepository _repository;

        public DeleteCartHandler(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (cart is null)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }

}
