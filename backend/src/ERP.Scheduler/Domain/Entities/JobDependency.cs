using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class JobDependency : AuditableEntity
{
    public long ScheduledJobId { get; private set; }
    public long DependsOnJobId { get; private set; }
    public short DependencyType { get; private set; }
    public bool IsMandatory { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ScheduledJob? Job { get; private set; }
    public ScheduledJob? DependsOnJob { get; private set; }
}
