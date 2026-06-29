using ERP.SharedKernel.Base;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetTimeline : AuditableEntity
{
    public long AssetId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Asset? Asset { get; private set; }
}
