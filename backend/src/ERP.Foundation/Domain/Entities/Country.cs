using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Country : AggregateRoot
{
    public string CountryCode { get; private set; } = string.Empty;
    public string CountryCode3 { get; private set; } = string.Empty;
    public string CountryName { get; private set; } = string.Empty;
    public string? Nationality { get; private set; }
    public string? PhoneCode { get; private set; }
    public long? CurrencyId { get; private set; }
    public string? TimeZone { get; private set; }
    public string DateFormat { get; private set; } = "dd/MM/yyyy";
    public bool IsActive { get; private set; }
    public int DisplayOrder { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Currency? Currency { get; private set; }
    public ICollection<StateProvince> StateProvinces { get; private set; } = [];
    public ICollection<City> Cities { get; private set; } = [];
}
