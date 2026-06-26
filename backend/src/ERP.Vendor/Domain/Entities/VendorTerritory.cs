using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorTerritory : AuditableEntity
{
    public string TerritoryCode { get; private set; } = string.Empty;
    public string TerritoryName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Vendor> Vendors { get; private set; } = [];
}
