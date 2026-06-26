using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerNote : AuditableEntity
{
    public long CustomerId { get; private set; }
    public long NoteId { get; private set; }
    public string NoteCategory { get; private set; } = string.Empty;
    public bool IsPinned { get; private set; }
    public bool IsConfidential { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
