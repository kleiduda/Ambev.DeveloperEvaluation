using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.DiscountStrategies;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
    {
        private readonly ICartRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateCartHandler(ICartRepository repository, IProductRepository productRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Cart>(command);

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

            var created = await _repository.CreateAsync(cart, cancellationToken);

            return _mapper.Map<CreateCartResult>(created);
        }
    }

}
