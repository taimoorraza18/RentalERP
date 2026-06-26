using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerTaxProfile : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string TaxProfileCode { get; private set; } = string.Empty;
    public string TaxProfileName { get; private set; } = string.Empty;
    public string? NTN { get; private set; }
    public string? GSTNumber { get; private set; }
    public string TaxRegistrationType { get; private set; } = "Registered";
    public decimal DefaultTaxRate { get; private set; }
    public bool IsTaxExempt { get; private set; }
    public bool ReverseChargeApplicable { get; private set; }
    public string? Description { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Customer> Customers { get; private set; } = [];
}
