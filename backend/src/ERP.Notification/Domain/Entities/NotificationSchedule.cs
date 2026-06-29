using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationSchedule : AuditableEntity
{
    public long NotificationId { get; private set; }
    public short ScheduleType { get; private set; }
    public string? CronExpression { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime? NextRunDate { get; private set; }
    public DateTime? LastRunDate { get; private set; }
    public bool IsEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Notification? Notification { get; private set; }
}
