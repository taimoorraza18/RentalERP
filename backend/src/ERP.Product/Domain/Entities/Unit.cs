using ERP.SharedKernel.Base;

namespace ERP.Product.Domain.Entities;

public sealed class Unit : AuditableEntity
{
    public string UnitCode { get; private set; } = string.Empty;
    public string UnitName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Item> Items { get; private set; } = [];
}
