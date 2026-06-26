using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorContact : AuditableEntity
{
    public long VendorId { get; private set; }
    public long ContactId { get; private set; }
    public string ContactRole { get; private set; } = "Primary";
    public string? Department { get; private set; }
    public bool IsPrimary { get; private set; }
    public bool ReceiveNotifications { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
