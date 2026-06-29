using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationChannel : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string ChannelCode { get; private set; } = string.Empty;
    public string ChannelName { get; private set; } = string.Empty;
    public string? ProviderName { get; private set; }
    public string? ConfigurationJson { get; private set; }
    public bool IsEnabled { get; private set; }
    public int Priority { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
