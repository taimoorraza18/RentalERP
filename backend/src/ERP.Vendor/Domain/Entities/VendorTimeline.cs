using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorTimeline : AuditableEntity
{
    public long VendorId { get; private set; }
    public long TimelineId { get; private set; }
    public string? TimelineCategory { get; private set; }
    public bool IsSystemGenerated { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Vendor? Vendor { get; private set; }
}
