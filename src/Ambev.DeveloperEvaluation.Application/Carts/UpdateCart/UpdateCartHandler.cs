using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    /// <summary>
    /// Handles the update of a cart
    /// </summary>
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCartHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (cart is null)
                throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            cart.UserId = command.UserId;
            cart.Date = command.Date;
            cart.Products = _mapper.Map<List<CartProduct>>(command.Products);

            await _repository.UpdateAsync(cart, cancellationToken);

            return _mapper.Map<UpdateCartResult>(cart);
        }
    }

}
