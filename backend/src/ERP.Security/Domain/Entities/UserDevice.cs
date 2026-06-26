using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class UserDevice : AuditableEntity
{
    public long UserId { get; private set; }
    public string DeviceIdentifier { get; private set; } = string.Empty;
    public string? DeviceName { get; private set; }
    public string? OperatingSystem { get; private set; }
    public string? Browser { get; private set; }
    public DateTime FirstLoginDate { get; private set; }
    public DateTime LastLoginDate { get; private set; }
    public bool IsTrusted { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
