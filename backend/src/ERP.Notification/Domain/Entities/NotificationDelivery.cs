using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationDelivery : AuditableEntity
{
    public long NotificationId { get; private set; }
    public long NotificationRecipientId { get; private set; }
    public long NotificationChannelId { get; private set; }
    public short DeliveryStatus { get; private set; }
    public string? ProviderMessageId { get; private set; }
    public DateTime? SentDate { get; private set; }
    public DateTime? DeliveredDate { get; private set; }
    public int RetryCount { get; private set; }
    public string? FailureReason { get; private set; }
    public string? ProviderResponse { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Notification? Notification { get; private set; }
    public NotificationRecipient? NotificationRecipient { get; private set; }
    public NotificationChannel? NotificationChannel { get; private set; }
}
