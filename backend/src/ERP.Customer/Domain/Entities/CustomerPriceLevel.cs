using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerPriceLevel : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string PriceLevelCode { get; private set; } = string.Empty;
    public string PriceLevelName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal MarkupPercentage { get; private set; }
    public int Priority { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Customer> Customers { get; private set; } = [];
}
