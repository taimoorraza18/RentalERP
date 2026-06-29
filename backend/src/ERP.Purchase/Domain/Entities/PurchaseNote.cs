using ERP.SharedKernel.Base;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseNote : AuditableEntity
{
    public long PurchaseOrderId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PurchaseOrder? PurchaseOrder { get; private set; }
}
