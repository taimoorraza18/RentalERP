using ERP.SharedKernel.Base;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;

namespace ERP.Service.Domain.Entities;

public sealed class ServiceSchedule : AuditableEntity
{
    public long AssetId { get; private set; }
    public short ScheduleType { get; private set; }
    public DateOnly ScheduledDate { get; private set; }
    public short? RecurrencePattern { get; private set; }
    public DateOnly? NextScheduledDate { get; private set; }
    public long? AssignedTo { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public AssetEntity? Asset { get; private set; }
}
