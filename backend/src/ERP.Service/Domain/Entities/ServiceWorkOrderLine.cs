using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceWorkOrderLine : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public string TaskDescription { get; private set; } = string.Empty;
    public decimal? EstimatedHours { get; private set; }
    public decimal? ActualHours { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
}
