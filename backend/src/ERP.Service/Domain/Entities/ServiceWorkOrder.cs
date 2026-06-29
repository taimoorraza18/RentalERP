using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceWorkOrder : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string WorkOrderNo { get; private set; } = string.Empty;
    public long? ServiceRequestId { get; private set; }
    public long AssetId { get; private set; }
    public DateOnly WorkOrderDate { get; private set; }
    public short WorkOrderType { get; private set; }
    public short Priority { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public DateOnly? ScheduledStartDate { get; private set; }
    public DateOnly? ScheduledEndDate { get; private set; }
    public DateOnly? ActualStartDate { get; private set; }
    public DateOnly? ActualEndDate { get; private set; }
    public long? AssignedTo { get; private set; }
    public long? CompletedBy { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public ServiceRequest? ServiceRequest { get; private set; }
    public AssetEntity? Asset { get; private set; }
    public ICollection<ServiceWorkOrderLine> ServiceWorkOrderLines { get; private set; } = [];
    public ICollection<ServiceTechnicianAssignment> ServiceTechnicianAssignments { get; private set; } = [];
    public ICollection<ServicePartConsumption> ServicePartConsumptions { get; private set; } = [];
    public ICollection<ServiceLabor> ServiceLabors { get; private set; } = [];
    public ICollection<ServiceCompletion> ServiceCompletions { get; private set; } = [];
    public ICollection<ServiceAttachment> ServiceAttachments { get; private set; } = [];
    public ICollection<ServiceNote> ServiceNotes { get; private set; } = [];
    public ICollection<ServiceActivity> ServiceActivities { get; private set; } = [];
    public ICollection<ServiceTimeline> ServiceTimelines { get; private set; } = [];
}
