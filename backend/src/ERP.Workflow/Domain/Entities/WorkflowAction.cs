using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowAction : AuditableEntity
{
    public long WorkflowInstanceId { get; private set; }
    public long WorkflowStepId { get; private set; }
    public long EmployeeId { get; private set; }
    public short ActionType { get; private set; }
    public DateTime ActionDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowInstance? WorkflowInstance { get; private set; }
    public WorkflowStep? WorkflowStep { get; private set; }
}
