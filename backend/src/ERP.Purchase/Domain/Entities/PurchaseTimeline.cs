using ERP.SharedKernel.Base;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseTimeline : AuditableEntity
{
    public long PurchaseOrderId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PurchaseOrder? PurchaseOrder { get; private set; }
}
