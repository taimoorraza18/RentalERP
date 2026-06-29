using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowStep : AuditableEntity
{
    public long WorkflowDefinitionId { get; private set; }
    public int StepNo { get; private set; }
    public string StepName { get; private set; } = string.Empty;
    public short ApprovalType { get; private set; }
    public int MinimumApprovals { get; private set; }
    public bool AutoApprove { get; private set; }
    public bool AllowReject { get; private set; }
    public bool AllowSkip { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowDefinition? WorkflowDefinition { get; private set; }
    public ICollection<WorkflowApprover> WorkflowApprovers { get; private set; } = [];
}
