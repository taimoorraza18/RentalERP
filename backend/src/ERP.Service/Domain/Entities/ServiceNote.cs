using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceNote : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
}
