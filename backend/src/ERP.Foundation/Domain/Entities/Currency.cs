using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Currency : AggregateRoot
{
    public string CurrencyCode { get; private set; } = string.Empty;
    public string CurrencyName { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;
    public long? CountryId { get; private set; }
    public byte DecimalPlaces { get; private set; }
    public byte RoundingMethod { get; private set; }
    public bool IsBaseCurrency { get; private set; }
    public bool IsActive { get; private set; }
    public int DisplayOrder { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Country? Country { get; private set; }
}
