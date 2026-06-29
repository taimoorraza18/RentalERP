using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class SystemSetting : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string SettingCode { get; private set; } = string.Empty;
    public string SettingName { get; private set; } = string.Empty;
    public string? SettingValue { get; private set; }
    public string DataType { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public bool IsSystem { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<FeatureFlag> FeatureFlags { get; private set; } = [];
    public ICollection<NumberSequence> NumberSequences { get; private set; } = [];
    public ICollection<Localization> Localizations { get; private set; } = [];
    public ICollection<CurrencyConfiguration> CurrencyConfigurations { get; private set; } = [];
    public ICollection<TaxConfiguration> TaxConfigurations { get; private set; } = [];
    public ICollection<ApplicationPreference> ApplicationPreferences { get; private set; } = [];
    public ICollection<ConfigurationAttachment> ConfigurationAttachments { get; private set; } = [];
    public ICollection<ConfigurationNote> ConfigurationNotes { get; private set; } = [];
    public ICollection<ConfigurationActivity> ConfigurationActivities { get; private set; } = [];
    public ICollection<ConfigurationTimeline> ConfigurationTimelines { get; private set; } = [];
}
