using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerAddress : AuditableEntity
{
    public long CustomerId { get; private set; }
    public long AddressId { get; private set; }
    public string AddressType { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public bool IsDefaultBilling { get; private set; }
    public bool IsDefaultShipping { get; private set; }
    public DateOnly? EffectiveFrom { get; private set; }
    public DateOnly? EffectiveTo { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
