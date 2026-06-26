using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerPaymentProfile : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string PaymentProfileCode { get; private set; } = string.Empty;
    public string PaymentProfileName { get; private set; } = string.Empty;
    public int PaymentTermDays { get; private set; }
    public decimal CreditLimit { get; private set; }
    public bool AllowPartialPayment { get; private set; }
    public bool AllowAdvancePayment { get; private set; }
    public int GracePeriodDays { get; private set; }
    public string? Description { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Customer> Customers { get; private set; } = [];
}
