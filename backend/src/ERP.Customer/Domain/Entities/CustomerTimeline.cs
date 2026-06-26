using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerTimeline : AuditableEntity
{
    public long CustomerId { get; private set; }
    public string EventType { get; private set; } = string.Empty;
    public string? ReferenceModule { get; private set; }
    public long? ReferenceId { get; private set; }
    public string? ReferenceNo { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime EventDate { get; private set; }
    public long PerformedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
