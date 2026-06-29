using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowDelegation : AuditableEntity
{
    public long WorkflowInstanceId { get; private set; }
    public long FromEmployeeId { get; private set; }
    public long ToEmployeeId { get; private set; }
    public string? DelegationReason { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime EffectiveTo { get; private set; }
    public bool IsRevoked { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowInstance? WorkflowInstance { get; private set; }
}
