using ERP.SharedKernel.Base;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceCompletion : AuditableEntity
{
    public long ServiceWorkOrderId { get; private set; }
    public DateOnly CompletionDate { get; private set; }
    public long CompletedBy { get; private set; }
    public decimal TotalLaborCost { get; private set; }
    public decimal TotalPartsCost { get; private set; }
    public decimal TotalExternalCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public string? CustomerSignature { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ServiceWorkOrder? ServiceWorkOrder { get; private set; }
}
