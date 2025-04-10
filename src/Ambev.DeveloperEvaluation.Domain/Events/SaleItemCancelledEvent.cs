namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public record SaleItemCancelledEvent(Guid SaleId, Guid ProductId, string? Justification);

}
