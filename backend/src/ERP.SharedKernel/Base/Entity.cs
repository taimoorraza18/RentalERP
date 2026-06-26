namespace ERP.SharedKernel.Base;

public abstract class Entity
{
    public long Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public long? UpdatedBy { get; private set; }
}
