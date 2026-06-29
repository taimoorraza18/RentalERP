using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Purchase.Domain.Entities;

public sealed class PurchaseRequisition : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string RequisitionNo { get; private set; } = string.Empty;
    public DateOnly RequisitionDate { get; private set; }
    public string? Department { get; private set; }
    public long RequestedBy { get; private set; }
    public DateOnly? RequiredDate { get; private set; }
    public bool IsApproved { get; private set; }
    public long? ApprovedBy { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public ICollection<PurchaseRequisitionLine> PurchaseRequisitionLines { get; private set; } = [];
}
