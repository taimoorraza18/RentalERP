using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class JobQueue : AuditableEntity
{
    public long ScheduledJobId { get; private set; }
    public string QueueName { get; private set; } = string.Empty;
    public short QueuePriority { get; private set; }
    public DateTime EnqueuedDate { get; private set; }
    public DateTime ScheduledExecutionDate { get; private set; }
    public short QueueStatus { get; private set; }
    public string? LockedByWorker { get; private set; }
    public DateTime? LockDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ScheduledJob? ScheduledJob { get; private set; }
}
