using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportNote : AuditableEntity
{
    public long ReportDefinitionId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportDefinition? ReportDefinition { get; private set; }
}
