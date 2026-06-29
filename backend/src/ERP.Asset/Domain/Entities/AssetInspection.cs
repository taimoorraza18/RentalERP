using ERP.SharedKernel.Base;

namespace ERP.Asset.Domain.Entities;

public sealed class AssetInspection : AuditableEntity
{
    public long AssetId { get; private set; }
    public long InspectorId { get; private set; }
    public DateOnly InspectionDate { get; private set; }
    public short InspectionType { get; private set; }
    public short Result { get; private set; }
    public decimal? OdometerReading { get; private set; }
    public decimal? HourMeterReading { get; private set; }
    public DateOnly? NextInspectionDate { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Asset? Asset { get; private set; }
}
