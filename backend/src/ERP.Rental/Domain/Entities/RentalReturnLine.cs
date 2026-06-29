using ERP.SharedKernel.Base;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalReturnLine : AuditableEntity
{
    public long RentalReturnId { get; private set; }
    public long AssetId { get; private set; }
    public DateOnly ExpectedReturnDate { get; private set; }
    public DateOnly ActualReturnDate { get; private set; }
    public int RentalDays { get; private set; }
    public int LateDays { get; private set; }
    public decimal? OdometerReading { get; private set; }
    public decimal? HourMeterReading { get; private set; }
    public short ReturnCondition { get; private set; }
    public decimal LateCharges { get; private set; }
    public decimal DamageCharges { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalReturn? RentalReturn { get; private set; }
    public AssetEntity? Asset { get; private set; }
}
