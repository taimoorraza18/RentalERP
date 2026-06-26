using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerActivity : AuditableEntity
{
    public long CustomerId { get; private set; }
    public long ActivityId { get; private set; }
    public string ActivityRole { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
