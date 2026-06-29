using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class FiscalPeriod : AuditableEntity
{
    public long FiscalYearId { get; private set; }
    public long CompanyId { get; private set; }
    public int PeriodNo { get; private set; }
    public string PeriodName { get; private set; } = string.Empty;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsOpen { get; private set; }
    public bool IsClosed { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public FiscalYear? FiscalYear { get; private set; }
    public Company? Company { get; private set; }
}
