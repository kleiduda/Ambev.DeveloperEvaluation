using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    /// <summary>
    /// Command to list paginated carts
    /// </summary>
    /// <param name="Page">Page number</param>
    /// <param name="PageSize">Page size</param>
    /// <param name="OrderBy">Ordering criteria</param>
    public record ListCartsCommand(int Page, int PageSize, string? OrderBy) : IRequest<ListCartsResult>;

}
