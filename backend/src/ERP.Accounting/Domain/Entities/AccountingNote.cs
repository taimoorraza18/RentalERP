using ERP.SharedKernel.Base;

namespace ERP.Accounting.Domain.Entities;

public sealed class AccountingNote : AuditableEntity
{
    public long JournalEntryId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public JournalEntry? JournalEntry { get; private set; }
}
