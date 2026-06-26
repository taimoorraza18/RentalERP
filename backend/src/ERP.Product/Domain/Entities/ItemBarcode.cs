using ERP.SharedKernel.Base;

namespace ERP.Product.Domain.Entities;

public sealed class ItemBarcode : AuditableEntity
{
    public long ItemId { get; private set; }
    public string Barcode { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Item? Item { get; private set; }
}
