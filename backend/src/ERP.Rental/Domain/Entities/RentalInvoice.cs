using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalInvoice : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string InvoiceNo { get; private set; } = string.Empty;
    public long RentalAgreementId { get; private set; }
    public long CustomerId { get; private set; }
    public DateOnly InvoiceDate { get; private set; }
    public DateOnly DueDate { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public decimal GrossAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal NetAmount { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public RentalAgreement? RentalAgreement { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public Currency? Currency { get; private set; }
}
