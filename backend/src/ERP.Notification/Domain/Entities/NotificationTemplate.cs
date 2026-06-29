using ERP.SharedKernel.Base;

namespace ERP.Notification.Domain.Entities;

public sealed class NotificationTemplate : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string TemplateCode { get; private set; } = string.Empty;
    public string TemplateName { get; private set; } = string.Empty;
    public string? Subject { get; private set; }
    public string MessageBody { get; private set; } = string.Empty;
    public string SupportedChannels { get; private set; } = string.Empty;
    public bool IsHtml { get; private set; }
    public bool IsSystem { get; private set; }
    public int VersionNo { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
