using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class StateProvince : AggregateRoot
{
    public long CountryId { get; private set; }
    public string StateCode { get; private set; } = string.Empty;
    public string StateName { get; private set; } = string.Empty;
    public string? ISOCode { get; private set; }
    public string? TaxRegionCode { get; private set; }
    public string? CapitalCity { get; private set; }
    public bool IsActive { get; private set; }
    public int DisplayOrder { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Country? Country { get; private set; }
    public ICollection<City> Cities { get; private set; } = [];
}
