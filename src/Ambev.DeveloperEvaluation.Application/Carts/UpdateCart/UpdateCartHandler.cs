using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies;
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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateCartHandler(ICartRepository repository,  IMapper mapper, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (cart is null)
                throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            cart.UserId = command.UserId;
            cart.Date = command.Date;
            cart.Products = _mapper.Map<List<CartProduct>>(command.Products);

            //Apply strategy discount
            decimal total = 0;
            foreach (var item in cart.Products)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {item.ProductId} not found");

                var strategy = DiscountStrategyResolver.Resolve(item.Quantity);
                var discounted = strategy.Apply(product.Price, item.Quantity);

                total += discounted;
            }

            cart.TotalValue = total;

            await _repository.UpdateAsync(cart, cancellationToken);

            return _mapper.Map<UpdateCartResult>(cart);
        }
    }

}
