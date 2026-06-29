using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class ApplicationPreference : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string PreferenceCode { get; private set; } = string.Empty;
    public string PreferenceName { get; private set; } = string.Empty;
    public string? PreferenceValue { get; private set; }
    public string DataType { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public bool IsEditable { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
