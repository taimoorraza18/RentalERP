using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class CurrencyConfiguration : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string CurrencyCode { get; private set; } = string.Empty;
    public string CurrencyName { get; private set; } = string.Empty;
    public string CurrencySymbol { get; private set; } = string.Empty;
    public short DecimalPlaces { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public bool IsBaseCurrency { get; private set; }
    public bool IsEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
