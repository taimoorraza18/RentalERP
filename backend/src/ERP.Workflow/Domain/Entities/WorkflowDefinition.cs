using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowDefinition : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string WorkflowCode { get; private set; } = string.Empty;
    public string WorkflowName { get; private set; } = string.Empty;
    public string ModuleName { get; private set; } = string.Empty;
    public string EntityName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsSystem { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<WorkflowStep> WorkflowSteps { get; private set; } = [];
    public ICollection<WorkflowInstance> WorkflowInstances { get; private set; } = [];
}
