using ERP.SharedKernel.Interfaces;

namespace ERP.SharedKernel.DomainEvents;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
