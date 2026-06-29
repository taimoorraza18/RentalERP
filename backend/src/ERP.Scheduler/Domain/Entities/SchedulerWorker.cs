using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class SchedulerWorker : AuditableEntity
{
    public string WorkerName { get; private set; } = string.Empty;
    public string ServerName { get; private set; } = string.Empty;
    public string? IPAddress { get; private set; }
    public string? WorkerVersion { get; private set; }
    public DateTime LastHeartbeat { get; private set; }
    public int ActiveJobs { get; private set; }
    public short WorkerStatus { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
