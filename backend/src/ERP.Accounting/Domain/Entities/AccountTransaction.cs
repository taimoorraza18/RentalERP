using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class AccountTransaction : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long AccountId { get; private set; }
    public long JournalEntryId { get; private set; }
    public long JournalEntryLineId { get; private set; }
    public DateOnly TransactionDate { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal RunningBalance { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Account? Account { get; private set; }
    public JournalEntry? JournalEntry { get; private set; }
    public JournalEntryLine? JournalEntryLine { get; private set; }
}
