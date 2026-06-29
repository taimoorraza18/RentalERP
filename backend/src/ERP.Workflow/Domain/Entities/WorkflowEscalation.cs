using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowEscalation : AuditableEntity
{
    public long WorkflowInstanceId { get; private set; }
    public long WorkflowStepId { get; private set; }
    public long? EscalatedToEmployeeId { get; private set; }
    public short EscalationLevel { get; private set; }
    public string? EscalationReason { get; private set; }
    public DateTime EscalationDate { get; private set; }
    public int SLAHours { get; private set; }
    public bool IsResolved { get; private set; }
    public DateTime? ResolvedDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowInstance? WorkflowInstance { get; private set; }
    public WorkflowStep? WorkflowStep { get; private set; }
}
