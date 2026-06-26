using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorTaxProfile : AuditableEntity
{
    public string TaxProfileCode { get; private set; } = string.Empty;
    public string TaxProfileName { get; private set; } = string.Empty;
    public bool SalesTaxApplicable { get; private set; }
    public bool WithholdingTaxApplicable { get; private set; }
    public decimal DefaultTaxRate { get; private set; }
    public string? TaxRegistrationNumber { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Vendor> Vendors { get; private set; } = [];
}
