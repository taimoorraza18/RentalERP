using ERP.SharedKernel.Base;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesTimeline : AuditableEntity
{
    public long SalesOrderId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SalesOrder? SalesOrder { get; private set; }
}
