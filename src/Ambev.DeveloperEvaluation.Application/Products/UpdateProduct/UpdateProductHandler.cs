using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (product is null)
                throw new KeyNotFoundException($"Product with ID {command.Id} not found");

            product.Title = command.Title;
            product.Price = command.Price;
            product.Description = command.Description;
            product.Category = command.Category;
            product.Image = command.Image;
            product.Rating = _mapper.Map<Rating>(command.Rating);

            await _repository.UpdateAsync(product, cancellationToken);

            return _mapper.Map<UpdateProductResult>(product);
        }
    }


}
