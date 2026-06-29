using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class JobExecution : AuditableEntity
{
    public long ScheduledJobId { get; private set; }
    public DateTime ExecutionStart { get; private set; }
    public DateTime? ExecutionEnd { get; private set; }
    public long DurationMilliseconds { get; private set; }
    public short ExecutionStatus { get; private set; }
    public string? ServerName { get; private set; }
    public string? WorkerName { get; private set; }
    public string? ErrorMessage { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ScheduledJob? ScheduledJob { get; private set; }
    public ICollection<JobRetry> JobRetries { get; private set; } = [];
}
