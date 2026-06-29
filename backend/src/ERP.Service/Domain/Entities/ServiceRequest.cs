using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceRequest : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string ServiceRequestNo { get; private set; } = string.Empty;
    public long AssetId { get; private set; }
    public long CustomerId { get; private set; }
    public DateOnly RequestDate { get; private set; }
    public short RequestType { get; private set; }
    public short Priority { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? ReportedIssue { get; private set; }
    public DateOnly? EstimatedStartDate { get; private set; }
    public DateOnly? EstimatedEndDate { get; private set; }
    public long RequestedBy { get; private set; }
    public long? AssignedTo { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public AssetEntity? Asset { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public ICollection<ServiceRequestLine> ServiceRequestLines { get; private set; } = [];
}
