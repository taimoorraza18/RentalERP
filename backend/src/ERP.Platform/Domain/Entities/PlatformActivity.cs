using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class PlatformActivity : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public long ActivityId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
}
