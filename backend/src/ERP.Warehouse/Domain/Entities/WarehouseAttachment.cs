using ERP.SharedKernel.Base;

namespace ERP.Warehouse.Domain.Entities;

public sealed class WarehouseAttachment : AuditableEntity
{
    public long WarehouseId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Warehouse? Warehouse { get; private set; }
}
