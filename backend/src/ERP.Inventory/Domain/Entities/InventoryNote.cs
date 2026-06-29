using ERP.SharedKernel.Base;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryNote : AuditableEntity
{
    public long InventoryTransactionId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public InventoryTransaction? InventoryTransaction { get; private set; }
}
