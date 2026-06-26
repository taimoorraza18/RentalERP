using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Timeline : AuditableEntity
{
    public string EventType { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string ReferenceModule { get; private set; } = string.Empty;
    public long ReferenceId { get; private set; }
    public string? ReferenceNo { get; private set; }
    public DateTime EventDate { get; private set; }
    public long PerformedBy { get; private set; }
    public short Severity { get; private set; }
    public bool IsSystemGenerated { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public User? PerformedByUser { get; private set; }
}
