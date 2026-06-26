using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorActivity : AuditableEntity
{
    public long VendorId { get; private set; }
    public long ActivityId { get; private set; }
    public string? ActivityCategory { get; private set; }
    public bool IsImportant { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
