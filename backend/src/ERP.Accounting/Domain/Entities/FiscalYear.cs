using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class FiscalYear : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string YearCode { get; private set; } = string.Empty;
    public string YearName { get; private set; } = string.Empty;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsOpen { get; private set; }
    public bool IsClosed { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public ICollection<FiscalPeriod> FiscalPeriods { get; private set; } = [];
}
