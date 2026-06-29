using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class Notification : AuditableEntity
{
    public long? NotificationTemplateId { get; private set; }
    public string NotificationType { get; private set; } = string.Empty;
    public string? Subject { get; private set; }
    public string MessageBody { get; private set; } = string.Empty;
    public short Priority { get; private set; }
    public string SourceModule { get; private set; } = string.Empty;
    public string SourceEntity { get; private set; } = string.Empty;
    public long SourceEntityId { get; private set; }
    public DateTime? ScheduledDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public NotificationTemplate? NotificationTemplate { get; private set; }
    public ICollection<NotificationRecipient> NotificationRecipients { get; private set; } = [];
    public ICollection<NotificationDelivery> NotificationDeliveries { get; private set; } = [];
    public ICollection<NotificationSchedule> NotificationSchedules { get; private set; } = [];
    public ICollection<NotificationAttachment> NotificationAttachments { get; private set; } = [];
    public ICollection<NotificationNote> NotificationNotes { get; private set; } = [];
    public ICollection<NotificationActivity> NotificationActivities { get; private set; } = [];
    public ICollection<NotificationTimeline> NotificationTimelines { get; private set; } = [];
}
