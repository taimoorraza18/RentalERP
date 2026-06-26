namespace ERP.SharedKernel.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; }
    long CreatedBy { get; }
    DateTime? UpdatedAt { get; }
    long? UpdatedBy { get; }
}
