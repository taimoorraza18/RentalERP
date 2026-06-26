using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Branch : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string BranchCode { get; private set; } = string.Empty;
    public string BranchName { get; private set; } = string.Empty;
    public long? ManagerUserId { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Address { get; private set; }
    public long? CityId { get; private set; }
    public long? StateProvinceId { get; private set; }
    public long? CountryId { get; private set; }
    public string? PostalCode { get; private set; }
    public bool IsHeadOffice { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public User? ManagerUser { get; private set; }
    public City? City { get; private set; }
    public StateProvince? StateProvince { get; private set; }
    public Country? Country { get; private set; }
}
