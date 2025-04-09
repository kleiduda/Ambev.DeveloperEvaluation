using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new cart
        /// </summary>
        /// <param name="request">Cart data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created cart</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCart(
            [FromBody] CreateCartRequest request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCartCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateCartResponse>(result);

            return Created(string.Empty, response);
        }


        /// <summary>
        /// Retrieves a cart by ID
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartById( [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetCartCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = $"Cart with ID {id} not found"
                });

            var response = _mapper.Map<GetCartResponse>(result);

            return Ok(new ApiResponseWithData<GetCartResponse>
            {
                Success = true,
                Data = response,
                Message = "Cart retrieved successfully"
            });
        }

        /// <summary>
        /// Updates a cart by ID
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="request">Updated cart data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated cart</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCart(
            [FromRoute] Guid id,
            [FromBody] UpdateCartRequest request,
            CancellationToken cancellationToken)
        {
            request.Id = id;

            var validator = new UpdateCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateCartCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<UpdateCartResponse>(result);

            return Ok(new ApiResponseWithData<UpdateCartResponse>
            {
                Success = true,
                Data = response,
                Message = "Cart updated successfully"
            });
        }


    }

}
