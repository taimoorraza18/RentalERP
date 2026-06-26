using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Permission : Entity
{
    public string PermissionKey { get; private set; } = string.Empty;
    public string Module { get; private set; } = string.Empty;
    public string Feature { get; private set; } = string.Empty;
    public string Action { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsSystemPermission { get; private set; }
    public int DisplayOrder { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
}
