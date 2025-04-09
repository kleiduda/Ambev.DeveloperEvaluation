using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Handles the logic to retrieve a cart by its ID
    /// </summary>
    public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult?>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public GetCartHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCartResult?> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return cart is null ? null : _mapper.Map<GetCartResult>(cart);
        }
    }


}
