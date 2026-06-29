using ERP.SharedKernel.Base;

namespace ERP.Reporting.Domain.Entities;

public sealed class ReportCategory : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string CategoryCode { get; private set; } = string.Empty;
    public string CategoryName { get; private set; } = string.Empty;
    public string? Icon { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsSystem { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<ReportDefinition> ReportDefinitions { get; private set; } = [];
}
