using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorPaymentProfile : AuditableEntity
{
    public string ProfileCode { get; private set; } = string.Empty;
    public string ProfileName { get; private set; } = string.Empty;
    public int PaymentTermDays { get; private set; }
    public decimal AdvancePaymentPercent { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Vendor> Vendors { get; private set; } = [];
}
