using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new prduct
        /// </summary>
        /// <param name="request">Product data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);
            var response = _mapper.Map<CreateProductResponse>(result);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<ListProductsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(
        [FromQuery] int _page = 1,
        [FromQuery] int _size = 10,
        [FromQuery(Name = "_order")] string? order = null,
        CancellationToken cancellationToken = default)
        {
            var request = new ListProductsRequest
            {
                Page = _page,
                PageSize = _size,
                OrderBy = order
            };

            var validator = new ListProductsRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListProductsCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<List<ListProductsResponse>>(result.Items);

            var paginatedList = new PaginatedList<ListProductsResponse>(
                response,
                result.TotalItems,
                result.CurrentPage,
                result.PageSize
            );

            return OkPaginated(paginatedList);
        }


        /// <summary>
        /// Gets a product by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetProductCommand(id);

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
                return NotFound($"Product with ID {id} not found");

            var response = _mapper.Map<GetProductResponse>(result);
            return Ok(response);
        }

        /// <summary>
        /// Updates a product by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,[FromBody] UpdateProductRequest request,CancellationToken cancellationToken)
        {
            request.Id = id;

            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<UpdateProductResponse>(result);

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = response
            });
        }


        /// <summary>
        /// Deletes a product by ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }

        /// <summary>
        /// Retrieves all unique product categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of category names</returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);

            return Ok(new ApiResponseWithData<List<string>>
            {
                Success = true,
                Data = categories,
                Message = "Categories retrieved successfully"
            });
        }


    }

}
