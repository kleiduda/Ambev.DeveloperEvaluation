using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Categories
{
    public record ListByCategoryCommand(string Category, int Page, int PageSize, string? OrderBy)
    : IRequest<ListByCategoryResult>;

}
