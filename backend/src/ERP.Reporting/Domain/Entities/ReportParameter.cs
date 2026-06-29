using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportParameter : AuditableEntity
{
    public long ReportDefinitionId { get; private set; }
    public string ParameterName { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public short DataType { get; private set; }
    public string? LookupSource { get; private set; }
    public string? DefaultValue { get; private set; }
    public bool IsRequired { get; private set; }
    public bool AllowMultiple { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ReportDefinition? ReportDefinition { get; private set; }
}
