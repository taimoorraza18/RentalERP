using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class TaxConfiguration : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string TaxCode { get; private set; } = string.Empty;
    public string TaxName { get; private set; } = string.Empty;
    public decimal TaxRate { get; private set; }
    public short TaxType { get; private set; }
    public short CalculationMethod { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
