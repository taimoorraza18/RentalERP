using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class SchedulerActivity : AuditableEntity
{
    public long ScheduledJobId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ScheduledJob? ScheduledJob { get; private set; }
}
