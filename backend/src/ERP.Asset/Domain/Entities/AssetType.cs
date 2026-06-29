using ERP.SharedKernel.Base;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetType : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long AssetCategoryId { get; private set; }
    public string TypeCode { get; private set; } = string.Empty;
    public string TypeName { get; private set; } = string.Empty;
    public int? StandardLifeMonths { get; private set; }
    public decimal? DefaultDepreciationRate { get; private set; }
    public bool RequiresInspection { get; private set; }
    public bool RequiresMaintenance { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AssetCategory? AssetCategory { get; private set; }
    public ICollection<Asset> Assets { get; private set; } = [];
}
