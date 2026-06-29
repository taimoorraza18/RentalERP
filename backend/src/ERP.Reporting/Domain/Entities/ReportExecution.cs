using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportExecution : AuditableEntity
{
    public long ReportDefinitionId { get; private set; }
    public long UserId { get; private set; }
    public DateTime ExecutionDate { get; private set; }
    public string? ParameterJson { get; private set; }
    public int DurationMs { get; private set; }
    public int RecordCount { get; private set; }
    public short ExecutionStatus { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? ExportFormat { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportDefinition? ReportDefinition { get; private set; }
    public ICollection<ReportExport> ReportExports { get; private set; } = [];
}
