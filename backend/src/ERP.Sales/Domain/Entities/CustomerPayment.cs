using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using Account = ERP.Accounting.Domain.Entities.Account;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Sales.Domain.Entities;

public sealed class CustomerPayment : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string PaymentNo { get; private set; } = string.Empty;
    public DateOnly PaymentDate { get; private set; }
    public long CustomerId { get; private set; }
    public long? SalesInvoiceId { get; private set; }
    public short PaymentMethod { get; private set; }
    public long? DepositAccountId { get; private set; }
    public string? ChequeNo { get; private set; }
    public DateOnly? ChequeDate { get; private set; }
    public decimal Amount { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public SalesInvoice? SalesInvoice { get; private set; }
    public Currency? Currency { get; private set; }
    public Account? DepositAccount { get; private set; }
}
