using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Role : AggregateRoot
{
    public long CompanyId { get; private set; }
    public string RoleCode { get; private set; } = string.Empty;
    public string RoleName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsSystemRole { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
    public ICollection<UserRole> UserRoles { get; private set; } = [];
}
