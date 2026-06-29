using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using WarehouseEntity = ERP.Warehouse.Domain.Entities.Warehouse;

namespace ERP.Inventory.Domain.Entities;

public sealed class InventoryTransaction : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public long WarehouseId { get; private set; }
    public string MovementNo { get; private set; } = string.Empty;
    public DateTime MovementDate { get; private set; }
    public short MovementType { get; private set; }
    public short? SourceDocumentType { get; private set; }
    public long? SourceDocumentId { get; private set; }
    public string? ReferenceNo { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public WarehouseEntity? Warehouse { get; private set; }
    public ICollection<InventoryTransactionLine> InventoryTransactionLines { get; private set; } = [];
    public ICollection<InventoryAttachment> InventoryAttachments { get; private set; } = [];
    public ICollection<InventoryNote> InventoryNotes { get; private set; } = [];
    public ICollection<InventoryActivity> InventoryActivities { get; private set; } = [];
    public ICollection<InventoryTimeline> InventoryTimelines { get; private set; } = [];
}
