using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;
using CustomerPaymentProfile = ERP.Customer.Domain.Entities.CustomerPaymentProfile;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalQuotation : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string QuotationNo { get; private set; } = string.Empty;
    public long CustomerId { get; private set; }
    public DateOnly QuotationDate { get; private set; }
    public DateOnly ValidUntil { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public long? PaymentProfileId { get; private set; }
    public DateOnly RentalStartDate { get; private set; }
    public DateOnly RentalEndDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal NetAmount { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public Currency? Currency { get; private set; }
    public CustomerPaymentProfile? PaymentProfile { get; private set; }
    public ICollection<RentalQuotationLine> RentalQuotationLines { get; private set; } = [];
}
