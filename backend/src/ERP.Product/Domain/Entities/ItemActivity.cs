using ERP.SharedKernel.Base;

namespace ERP.Product.Domain.Entities;

public sealed class ItemActivity : AuditableEntity
{
    public long ItemId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Item? Item { get; private set; }
}
