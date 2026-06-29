using ERP.SharedKernel.Base;

namespace ERP.Accounting.Domain.Entities;

public sealed class AccountingTimeline : AuditableEntity
{
    public long JournalEntryId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public JournalEntry? JournalEntry { get; private set; }
}
