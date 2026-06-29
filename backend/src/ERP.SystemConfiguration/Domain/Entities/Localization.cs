using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class Localization : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string CultureCode { get; private set; } = string.Empty;
    public string LanguageName { get; private set; } = string.Empty;
    public string? CurrencySymbol { get; private set; }
    public string DateFormat { get; private set; } = string.Empty;
    public string TimeFormat { get; private set; } = string.Empty;
    public string DecimalSeparator { get; private set; } = string.Empty;
    public string ThousandSeparator { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
