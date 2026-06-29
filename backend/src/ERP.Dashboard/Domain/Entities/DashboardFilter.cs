using ERP.SharedKernel.Base;

namespace ERP.Dashboard.Domain.Entities;

public sealed class DashboardFilter : AuditableEntity
{
    public long DashboardId { get; private set; }
    public string FilterName { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public short DataType { get; private set; }
    public string? DefaultValue { get; private set; }
    public string? LookupSource { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Dashboard? Dashboard { get; private set; }
}
