using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale record
        /// </summary>
        /// <param name="request">The sale data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Details of the created sale</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(result)
            });
        }

        /// <summary>
        /// Retrieves a paginated list of sales with optional filters and ordering
        /// </summary>
        /// <param name="_page">Page number</param>
        /// <param name="_size">Items per page</param>
        /// <param name="_order">Ordering (e.g., dataVenda desc)</param>
        /// <param name="clienteNome">Partial or exact match for client name</param>
        /// <param name="numeroVenda">Partial or exact match for sale number</param>
        /// <param name="_minDataVenda">Minimum sale date</param>
        /// <param name="_maxDataVenda">Maximum sale date</param>
        /// <param name="_minValorTotal">Minimum total value</param>
        /// <param name="_maxValorTotal">Maximum total value</param>
        /// <param name="cancelada">Filter by cancellation status</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<ListSalesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSales(
            [FromQuery(Name = "_page")] int _page = 1,
            [FromQuery(Name = "_size")] int _size = 10,
            [FromQuery(Name = "_order")] string? _order = null,
            [FromQuery] string? clienteNome = null,
            [FromQuery] string? numeroVenda = null,
            [FromQuery] DateTime? _minDataVenda = null,
            [FromQuery] DateTime? _maxDataVenda = null,
            [FromQuery] decimal? _minValorTotal = null,
            [FromQuery] decimal? _maxValorTotal = null,
            [FromQuery] bool? cancelada = null,
            CancellationToken cancellationToken = default)
        {
            var request = new ListSalesRequest
            {
                Page = _page,
                PageSize = _size,
                OrderBy = _order,
                ClienteNome = clienteNome,
                NumeroVenda = numeroVenda,
                _minDataVenda = _minDataVenda,
                _maxDataVenda = _maxDataVenda,
                _minValorTotal = _minValorTotal,
                _maxValorTotal = _maxValorTotal,
                Cancelada = cancelada
            };

            var validator = new ListSalesRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListSalesCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<List<ListSalesResponse>>(result.Items);

            var paginated = new PaginatedList<ListSalesResponse>(
                response,
                result.TotalItems,
                result.CurrentPage,
                result.PageSize
            );

            return OkPaginated(paginated);
        }

        /// <summary>
        /// Retrieves a sale by its ID with full details
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The full sale data including items</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new GetSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result is null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = $"Sale with ID {id} not found"
                });

            var response = _mapper.Map<GetSaleResponse>(result);

            return Ok(new ApiResponseWithData<GetSaleResponse>
            {
                Success = true,
                Data = response,
                Message = "Sale retrieved successfully"
            });
        }


    }

}
