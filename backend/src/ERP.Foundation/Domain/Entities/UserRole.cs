using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class UserRole : Entity
{
    public long UserId { get; private set; }
    public long RoleId { get; private set; }
    public bool DefaultRole { get; private set; }
    public DateTime? EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public User? User { get; private set; }
    public Role? Role { get; private set; }
}
