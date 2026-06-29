using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class JobRetry : AuditableEntity
{
    public long JobExecutionId { get; private set; }
    public short RetryNumber { get; private set; }
    public DateTime RetryDate { get; private set; }
    public string? RetryReason { get; private set; }
    public short RetryStatus { get; private set; }
    public DateTime? NextRetryDate { get; private set; }
    public string? ErrorMessage { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public JobExecution? JobExecution { get; private set; }
}
