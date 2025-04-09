﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Handles the deletion of a product
    /// </summary>
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteProductHandler"/> class
        /// </summary>
        /// <param name="repository">The product repository</param>
        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the deletion logic for a product
        /// </summary>
        /// <param name="request">The delete command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }

}
