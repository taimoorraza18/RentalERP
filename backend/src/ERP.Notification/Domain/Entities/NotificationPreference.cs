using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationPreference : AuditableEntity
{
    public long UserId { get; private set; }
    public string NotificationType { get; private set; } = string.Empty;
    public long NotificationChannelId { get; private set; }
    public bool IsEnabled { get; private set; }
    public TimeOnly? QuietHoursStart { get; private set; }
    public TimeOnly? QuietHoursEnd { get; private set; }
    public bool WeekendEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public NotificationChannel? NotificationChannel { get; private set; }
}
