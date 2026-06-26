using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Company : AuditableEntity
{
    public string CompanyCode { get; private set; } = string.Empty;
    public string CompanyName { get; private set; } = string.Empty;
    public string LegalName { get; private set; } = string.Empty;
    public string? NTN { get; private set; }
    public string? STRN { get; private set; }
    public long CurrencyId { get; private set; }
    public long FiscalYearId { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Website { get; private set; }
    public string? LogoPath { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? Country { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Currency? Currency { get; private set; }
    public FiscalYear? CurrentFiscalYear { get; private set; }
    public ICollection<Branch> Branches { get; private set; } = [];
    public ICollection<FiscalYear> FiscalYears { get; private set; } = [];
    public ICollection<NumberSeries> NumberSeries { get; private set; } = [];
}
