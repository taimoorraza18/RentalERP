using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowApprover : AuditableEntity
{
    public long WorkflowStepId { get; private set; }
    public short ApproverType { get; private set; }
    public long? EmployeeId { get; private set; }
    public long? RoleId { get; private set; }
    public long? DepartmentId { get; private set; }
    public long? DesignationId { get; private set; }
    public string? DynamicExpression { get; private set; }
    public short Priority { get; private set; }
    public bool IsMandatory { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowStep? WorkflowStep { get; private set; }
}
