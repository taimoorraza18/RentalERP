using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class AccountBalance : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long FiscalYearId { get; private set; }
    public long FiscalPeriodId { get; private set; }
    public long AccountId { get; private set; }
    public decimal OpeningDebit { get; private set; }
    public decimal OpeningCredit { get; private set; }
    public decimal PeriodDebit { get; private set; }
    public decimal PeriodCredit { get; private set; }
    public decimal ClosingDebit { get; private set; }
    public decimal ClosingCredit { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public FiscalYear? FiscalYear { get; private set; }
    public FiscalPeriod? FiscalPeriod { get; private set; }
    public Account? Account { get; private set; }
}
