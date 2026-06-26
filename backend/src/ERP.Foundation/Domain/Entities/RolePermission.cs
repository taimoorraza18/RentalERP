using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class RolePermission : Entity
{
    public long RoleId { get; private set; }
    public long PermissionId { get; private set; }
    public bool IsAllowed { get; private set; }
    public DateTime? EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Role? Role { get; private set; }
    public Permission? Permission { get; private set; }
}
