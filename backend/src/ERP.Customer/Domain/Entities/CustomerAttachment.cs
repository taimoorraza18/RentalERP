using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerAttachment : AuditableEntity
{
    public long CustomerId { get; private set; }
    public long AttachmentId { get; private set; }
    public string AttachmentCategory { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsPrimary { get; private set; }
    public DateOnly? ExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
