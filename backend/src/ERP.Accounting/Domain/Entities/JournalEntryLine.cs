using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class JournalEntryLine : AuditableEntity
{
    public long JournalEntryId { get; private set; }
    public long AccountId { get; private set; }
    public long? CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public long? CostCenterId { get; private set; }
    public long? ProjectId { get; private set; }
    public string? Narration { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public JournalEntry? JournalEntry { get; private set; }
    public Account? Account { get; private set; }
    public Currency? Currency { get; private set; }
}
