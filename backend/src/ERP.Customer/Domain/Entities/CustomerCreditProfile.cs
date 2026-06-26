using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerCreditProfile : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string CreditProfileCode { get; private set; } = string.Empty;
    public string CreditProfileName { get; private set; } = string.Empty;
    public decimal DefaultCreditLimit { get; private set; }
    public decimal MaximumOutstanding { get; private set; }
    public int MaximumOverdueDays { get; private set; }
    public bool RequireApprovalOverLimit { get; private set; }
    public bool AllowCreditHoldOverride { get; private set; }
    public string? Description { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Customer> Customers { get; private set; } = [];
}
