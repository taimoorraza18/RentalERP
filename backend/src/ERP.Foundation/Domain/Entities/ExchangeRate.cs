using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class ExchangeRate : AggregateRoot
{
    public long CompanyId { get; private set; }
    public long FromCurrencyId { get; private set; }
    public long ToCurrencyId { get; private set; }
    public DateOnly RateDate { get; private set; }
    public decimal Rate { get; private set; }
    public decimal? BuyingRate { get; private set; }
    public decimal? SellingRate { get; private set; }
    public string? Source { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Currency? FromCurrency { get; private set; }
    public Currency? ToCurrency { get; private set; }
}
