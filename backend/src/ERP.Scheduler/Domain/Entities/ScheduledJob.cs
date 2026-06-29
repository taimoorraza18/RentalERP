using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class ScheduledJob : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string JobCode { get; private set; } = string.Empty;
    public string JobName { get; private set; } = string.Empty;
    public short JobType { get; private set; }
    public string? CronExpression { get; private set; }
    public DateTime? NextExecutionDate { get; private set; }
    public DateTime? LastExecutionDate { get; private set; }
    public bool IsEnabled { get; private set; }
    public short Priority { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<JobExecution> JobExecutions { get; private set; } = [];
    public ICollection<JobParameter> JobParameters { get; private set; } = [];
    public ICollection<JobQueue> JobQueues { get; private set; } = [];
    public ICollection<JobDependency> JobDependencies { get; private set; } = [];
    public ICollection<SchedulerAttachment> SchedulerAttachments { get; private set; } = [];
    public ICollection<SchedulerNote> SchedulerNotes { get; private set; } = [];
    public ICollection<SchedulerActivity> SchedulerActivities { get; private set; } = [];
    public ICollection<SchedulerTimeline> SchedulerTimelines { get; private set; } = [];
}
