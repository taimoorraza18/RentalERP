using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class FiscalYear : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string FiscalYearCode { get; private set; } = string.Empty;
    public string FiscalYearName { get; private set; } = string.Empty;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public byte Status { get; private set; }
    public bool IsDefault { get; private set; }
    public bool AllowBackDatedEntries { get; private set; }
    public DateOnly? ClosingDate { get; private set; }
    public long? ClosedBy { get; private set; }
    public DateTime? ClosedDate { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public User? ClosedByUser { get; private set; }
}
