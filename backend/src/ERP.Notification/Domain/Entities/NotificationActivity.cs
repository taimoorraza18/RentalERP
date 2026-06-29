using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationActivity : AuditableEntity
{
    public long NotificationId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Notification? Notification { get; private set; }
}
