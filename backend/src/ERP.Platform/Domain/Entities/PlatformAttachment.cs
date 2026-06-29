using ERP.SharedKernel.Base;

namespace ERP.Platform.Domain.Entities;

public sealed class PlatformAttachment : AuditableEntity
{
    public long PlatformEnvironmentId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public PlatformEnvironment? PlatformEnvironment { get; private set; }
}
