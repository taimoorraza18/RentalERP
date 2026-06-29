using ERP.SharedKernel.Base;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalDamage : AuditableEntity
{
    public long RentalReturnId { get; private set; }
    public long RentalReturnLineId { get; private set; }
    public long AssetId { get; private set; }
    public short DamageType { get; private set; }
    public short Severity { get; private set; }
    public decimal EstimatedRepairCost { get; private set; }
    public decimal CustomerCharge { get; private set; }
    public decimal InsuranceClaimAmount { get; private set; }
    public string? Description { get; private set; }
    public bool IsRepaired { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalReturn? RentalReturn { get; private set; }
    public RentalReturnLine? RentalReturnLine { get; private set; }
    public AssetEntity? Asset { get; private set; }
}
