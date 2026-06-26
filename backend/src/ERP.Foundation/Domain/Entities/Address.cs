using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Address : AuditableEntity
{
    public string AddressType { get; private set; } = string.Empty;
    public string AddressLine1 { get; private set; } = string.Empty;
    public string? AddressLine2 { get; private set; }
    public long CountryId { get; private set; }
    public long? StateProvinceId { get; private set; }
    public long? CityId { get; private set; }
    public string? PostalCode { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public string? GooglePlaceId { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Country? Country { get; private set; }
    public StateProvince? StateProvince { get; private set; }
    public City? City { get; private set; }
}
