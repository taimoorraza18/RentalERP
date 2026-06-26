using ERP.SharedKernel.Base;

namespace ERP.Product.Domain.Entities;

public sealed class ItemImage : AuditableEntity
{
    public long ItemId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsPrimary { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Item? Item { get; private set; }
}
