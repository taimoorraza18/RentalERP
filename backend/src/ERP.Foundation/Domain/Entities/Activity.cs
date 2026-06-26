using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Activity : AuditableEntity
{
    public string ActivityType { get; private set; } = string.Empty;
    public string Subject { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public short Priority { get; private set; }
    public long? AssignedTo { get; private set; }
    public long? CompletedBy { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public User? AssignedToUser { get; private set; }
    public User? CompletedByUser { get; private set; }
}
