using ERP.SharedKernel.Base;

namespace ERP.SystemConfiguration.Domain.Entities;

public sealed class NumberSequence : AuditableEntity
{
    public long SystemSettingId { get; private set; }
    public string SequenceCode { get; private set; } = string.Empty;
    public string SequenceName { get; private set; } = string.Empty;
    public string? Prefix { get; private set; }
    public string? Suffix { get; private set; }
    public long CurrentNumber { get; private set; }
    public int IncrementBy { get; private set; }
    public short NumberLength { get; private set; }
    public short ResetPolicy { get; private set; }
    public DateTime? LastResetDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SystemSetting? SystemSetting { get; private set; }
}
