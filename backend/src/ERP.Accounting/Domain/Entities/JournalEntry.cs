using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class JournalEntry : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long VoucherTypeId { get; private set; }
    public long FiscalYearId { get; private set; }
    public long FiscalPeriodId { get; private set; }
    public string VoucherNo { get; private set; } = string.Empty;
    public DateOnly VoucherDate { get; private set; }
    public string? ReferenceNo { get; private set; }
    public string? Narration { get; private set; }
    public decimal TotalDebit { get; private set; }
    public decimal TotalCredit { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public bool IsReversed { get; private set; }
    public long? ReversalJournalEntryId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public VoucherType? VoucherType { get; private set; }
    public FiscalYear? FiscalYear { get; private set; }
    public FiscalPeriod? FiscalPeriod { get; private set; }
    public JournalEntry? ReversalJournalEntry { get; private set; }
    public ICollection<JournalEntryLine> JournalEntryLines { get; private set; } = [];
    public ICollection<AccountingAttachment> AccountingAttachments { get; private set; } = [];
    public ICollection<AccountingNote> AccountingNotes { get; private set; } = [];
    public ICollection<AccountingActivity> AccountingActivities { get; private set; } = [];
    public ICollection<AccountingTimeline> AccountingTimelines { get; private set; } = [];
}
