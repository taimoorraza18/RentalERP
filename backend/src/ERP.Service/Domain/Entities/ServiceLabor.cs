using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceLabor : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public long TechnicianId { get; private set; }
    public DateOnly LaborDate { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public decimal HoursWorked { get; private set; }
    public decimal HourlyRate { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
}
