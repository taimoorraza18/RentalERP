using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerContact : AuditableEntity
{
    public long CustomerId { get; private set; }
    public long ContactId { get; private set; }
    public string ContactRole { get; private set; } = string.Empty;
    public bool IsPrimary { get; private set; }
    public bool ReceiveEmail { get; private set; }
    public bool ReceiveSMS { get; private set; }
    public bool ReceiveWhatsApp { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Customer? Customer { get; private set; }
}
