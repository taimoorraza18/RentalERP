using ERP.SharedKernel.Base;

namespace ERP.Customer.Domain.Entities;

public sealed class CustomerTerritory : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string TerritoryCode { get; private set; } = string.Empty;
    public string TerritoryName { get; private set; } = string.Empty;
    public long? ParentTerritoryId { get; private set; }
    public long? ManagerUserId { get; private set; }
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsDefault { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public CustomerTerritory? ParentTerritory { get; private set; }
    public ICollection<CustomerTerritory> Children { get; private set; } = [];
    public ICollection<Customer> Customers { get; private set; } = [];
}
