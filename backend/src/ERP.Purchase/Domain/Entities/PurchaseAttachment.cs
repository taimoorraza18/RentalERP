using ERP.SharedKernel.Base;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseAttachment : AuditableEntity
{
    public long PurchaseOrderId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PurchaseOrder? PurchaseOrder { get; private set; }
}
