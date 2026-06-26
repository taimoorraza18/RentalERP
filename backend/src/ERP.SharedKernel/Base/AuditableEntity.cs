namespace ERP.SharedKernel.Base;

public abstract class AuditableEntity : AggregateRoot
{
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public long? DeletedBy { get; private set; }
}
