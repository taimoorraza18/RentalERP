using ERP.SharedKernel.Base;

namespace ERP.Product.Domain.Entities;

public sealed class ItemAttachment : AuditableEntity
{
    public long ItemId { get; private set; }
    public long AttachmentId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Item? Item { get; private set; }
}
