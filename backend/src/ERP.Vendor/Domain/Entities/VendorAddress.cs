using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorAddress : AuditableEntity
{
    public long VendorId { get; private set; }
    public long AddressId { get; private set; }
    public string AddressType { get; private set; } = "Billing";
    public bool IsPrimary { get; private set; }
    public bool IsDefaultBilling { get; private set; }
    public bool IsDefaultShipping { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
