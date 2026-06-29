using ERP.SharedKernel.Base;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesAttachment : AuditableEntity
{
    public long SalesOrderId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SalesOrder? SalesOrder { get; private set; }
}
