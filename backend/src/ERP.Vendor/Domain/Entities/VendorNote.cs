using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorNote : AuditableEntity
{
    public long VendorId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
