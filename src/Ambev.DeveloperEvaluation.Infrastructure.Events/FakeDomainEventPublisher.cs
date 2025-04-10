using Ambev.DeveloperEvaluation.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Infrastructure.Events
{
    /// <summary>
    /// Simulates a domain event publisher for development or testing purposes.
    /// This implementation logs event information instead of sending it to a message broker.
    /// 
    /// It's used as a placeholder for a real event publisher (e.g., Kafka, RabbitMQ),
    /// allowing the application to emit integration events without requiring infrastructure dependencies.
    ///
    /// This approach ensures that domain events like SaleCreated, SaleCancelled, etc., can still
    /// be handled in a decoupled manner, providing flexibility for future integration.
    ///
    /// Example log format:
    /// [DOMAIN EVENT] SaleCreatedEvent: { "SaleId": "...", "SaleNumber": "...", "SaleDate": "..." }
    /// </summary>

    public class FakeDomainEventPublisher : IDomainEventPublisher
    {
        private readonly ILogger<FakeDomainEventPublisher> _logger;

        public FakeDomainEventPublisher(ILogger<FakeDomainEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
        {
            var eventName = typeof(TEvent).Name;
            var payload = JsonSerializer.Serialize(domainEvent);

            _logger.LogInformation("[DOMAIN EVENT] {Event}: {Payload}", eventName, payload);

            return Task.CompletedTask;
        }
    }

}
