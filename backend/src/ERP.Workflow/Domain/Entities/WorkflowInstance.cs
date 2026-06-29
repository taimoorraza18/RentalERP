using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowInstance : AuditableEntity
{
    public long WorkflowDefinitionId { get; private set; }
    public string EntityName { get; private set; } = string.Empty;
    public long EntityId { get; private set; }
    public long? CurrentStepId { get; private set; }
    public short WorkflowStatus { get; private set; }
    public DateTime StartedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public long InitiatedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowDefinition? WorkflowDefinition { get; private set; }
    public WorkflowStep? CurrentStep { get; private set; }
    public ICollection<WorkflowAction> WorkflowActions { get; private set; } = [];
    public ICollection<WorkflowEscalation> WorkflowEscalations { get; private set; } = [];
    public ICollection<WorkflowDelegation> WorkflowDelegations { get; private set; } = [];
    public ICollection<WorkflowAttachment> WorkflowAttachments { get; private set; } = [];
    public ICollection<WorkflowNote> WorkflowNotes { get; private set; } = [];
    public ICollection<WorkflowActivity> WorkflowActivities { get; private set; } = [];
    public ICollection<WorkflowTimeline> WorkflowTimelines { get; private set; } = [];
}
