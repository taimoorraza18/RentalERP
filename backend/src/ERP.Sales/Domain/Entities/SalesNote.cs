using ERP.SharedKernel.Base;

namespace ERP.Sales.Domain.Entities;

public sealed class SalesNote : AuditableEntity
{
    public long SalesOrderId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public SalesOrder? SalesOrder { get; private set; }
}
