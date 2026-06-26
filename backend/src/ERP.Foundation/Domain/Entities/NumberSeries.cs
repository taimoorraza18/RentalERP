using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class NumberSeries : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long? BranchId { get; private set; }
    public string ModuleName { get; private set; } = string.Empty;
    public string DocumentType { get; private set; } = string.Empty;
    public string SeriesName { get; private set; } = string.Empty;
    public string? Prefix { get; private set; }
    public string? Suffix { get; private set; }
    public string Separator { get; private set; } = "-";
    public long NextNumber { get; private set; }
    public short NumberLength { get; private set; }
    public string ResetPolicy { get; private set; } = "Never";
    public long? FiscalYearId { get; private set; }
    public bool IsDefault { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public FiscalYear? FiscalYear { get; private set; }
}
