using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceActivity : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
}
