using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceRequestLine : AuditableEntity
{
    public long ServiceRequestId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal? EstimatedHours { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceRequest? ServiceRequest { get; private set; }
}
