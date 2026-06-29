using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class SavedReport : AuditableEntity
{
    public long ReportDefinitionId { get; private set; }
    public long UserId { get; private set; }
    public string ReportName { get; private set; } = string.Empty;
    public string ParameterJson { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }
    public bool IsPublic { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportDefinition? ReportDefinition { get; private set; }
}
