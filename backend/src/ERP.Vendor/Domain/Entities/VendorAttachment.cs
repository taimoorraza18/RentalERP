using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorAttachment : AuditableEntity
{
    public long VendorId { get; private set; }
    public long AttachmentId { get; private set; }
    public string AttachmentType { get; private set; } = "General";
    public bool IsPrimary { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
