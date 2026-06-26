using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class City : AggregateRoot
{
    public long CountryId { get; private set; }
    public long? StateProvinceId { get; private set; }
    public string CityCode { get; private set; } = string.Empty;
    public string CityName { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public int DisplayOrder { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Country? Country { get; private set; }
    public StateProvince? StateProvince { get; private set; }
}
