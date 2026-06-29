using ERP.SharedKernel.Base;
using ERP.Inventory.Domain.Entities;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalReservationLine : AuditableEntity
{
    public long RentalReservationId { get; private set; }
    public long AssetId { get; private set; }
    public long? InventoryReservationId { get; private set; }
    public int RentalDays { get; private set; }
    public decimal DailyRate { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalReservation? RentalReservation { get; private set; }
    public AssetEntity? Asset { get; private set; }
    public InventoryReservation? InventoryReservation { get; private set; }
}
