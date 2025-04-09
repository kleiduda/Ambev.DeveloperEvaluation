using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCart;
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
        /// Retrieves a paginated list of carts
        /// </summary>
        /// <param name="_page">Page number (default: 1)</param>
        /// <param name="_size">Number of items per page (default: 10)</param>
        /// <param name="order">Ordering of results</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of carts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<ListCartsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCarts(
            [FromQuery(Name = "_page")] int _page = 1,
            [FromQuery(Name = "_size")] int _size = 10,
            [FromQuery(Name = "_order")] string? order = null,
            CancellationToken cancellationToken = default)
        {
            var request = new ListCartsRequest
            {
                Page = _page,
                PageSize = _size,
                OrderBy = order
            };

            var validator = new ListCartsRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListCartsCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<List<ListCartsResponse>>(result.Items);

            var paginated = new PaginatedList<ListCartsResponse>(
                response,
                result.TotalItems,
                result.CurrentPage,
                result.PageSize
            );

            return OkPaginated(paginated);
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


        /// <summary>
        /// Deletes a cart by ID
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success message if the cart is deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var request = new DeleteCartRequest { Id = id };
            var validator = new DeleteCartRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCartCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Cart deleted successfully"
            });
        }


    }

}
