using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalReturn : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public long RentalCheckInId { get; private set; }
    public string ReturnNo { get; private set; } = string.Empty;
    public DateTime ReturnDate { get; private set; }
    public short InspectionResult { get; private set; }
    public decimal AdditionalCharges { get; private set; }
    public decimal DamageCharges { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
    public RentalCheckIn? RentalCheckIn { get; private set; }
    public ICollection<RentalReturnLine> RentalReturnLines { get; private set; } = [];
    public ICollection<RentalDamage> RentalDamages { get; private set; } = [];
}
