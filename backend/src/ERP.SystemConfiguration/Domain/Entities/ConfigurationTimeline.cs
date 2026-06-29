using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class ConfigurationTimeline : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
