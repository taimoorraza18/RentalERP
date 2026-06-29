using ERP.SharedKernel.Base;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetCategory : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string CategoryCode { get; private set; } = string.Empty;
    public string CategoryName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsSystemDefined { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Asset> Assets { get; private set; } = [];
}
