using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationRecipient : AuditableEntity
{
    public long NotificationId { get; private set; }
    public short RecipientType { get; private set; }
    public long? RecipientId { get; private set; }
    public string? RecipientName { get; private set; }
    public string? Email { get; private set; }
    public string? Mobile { get; private set; }
    public bool IsRead { get; private set; }
    public DateTime? ReadDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Notification? Notification { get; private set; }
    public ICollection<NotificationDelivery> NotificationDeliveries { get; private set; } = [];
}
