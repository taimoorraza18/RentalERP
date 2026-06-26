namespace ERP.SharedKernel.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
    long? DeletedBy { get; }
}
