using ERP.SharedKernel.Base;

namespace ERP.Workflow.Domain.Entities;

public sealed class WorkflowAttachment : AuditableEntity
{
    public long WorkflowInstanceId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public WorkflowInstance? WorkflowInstance { get; private set; }
}
