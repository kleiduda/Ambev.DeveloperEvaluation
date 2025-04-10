namespace Ambev.DeveloperEvaluation.Application.Interfaces
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default);
    }

}
