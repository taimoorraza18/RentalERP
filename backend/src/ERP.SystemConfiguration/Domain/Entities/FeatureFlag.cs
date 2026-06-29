using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class FeatureFlag : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string FeatureCode { get; private set; } = string.Empty;
    public string FeatureName { get; private set; } = string.Empty;
    public bool IsEnabled { get; private set; }
    public decimal RolloutPercentage { get; private set; }
    public DateTime? EffectiveDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
