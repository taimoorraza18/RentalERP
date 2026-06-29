using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationTimeline : AuditableEntity
{
    public long NotificationId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Notification? Notification { get; private set; }
}
