using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class AuditLog : AggregateRoot
{
    public long? CompanyId { get; private set; }
    public long? BranchId { get; private set; }
    public string ModuleName { get; private set; } = string.Empty;
    public string EntityName { get; private set; } = string.Empty;
    public long? EntityId { get; private set; }
    public short ActionType { get; private set; }
    public string? Description { get; private set; }
    public string? OldValues { get; private set; }
    public string? NewValues { get; private set; }
    public string? IPAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public long? PerformedBy { get; private set; }
    public DateTime PerformedOn { get; private set; }
    public Guid TransactionId { get; private set; }
    public Guid? CorrelationId { get; private set; }

    public ICollection<AuditField> AuditFields { get; private set; } = [];
    public ICollection<EntityHistory> EntityHistories { get; private set; } = [];
    public ICollection<RestoreHistory> RestoreHistories { get; private set; } = [];
    public ICollection<AuditAttachment> AuditAttachments { get; private set; } = [];
    public ICollection<AuditNote> AuditNotes { get; private set; } = [];
    public ICollection<AuditActivity> AuditActivities { get; private set; } = [];
    public ICollection<AuditTimeline> AuditTimelines { get; private set; } = [];
}
