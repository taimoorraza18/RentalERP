using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorCategory : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string CategoryCode { get; private set; } = string.Empty;
    public string CategoryName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Vendor> Vendors { get; private set; } = [];
}
