using ERP.SharedKernel.Base;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetGroup : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string GroupCode { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
