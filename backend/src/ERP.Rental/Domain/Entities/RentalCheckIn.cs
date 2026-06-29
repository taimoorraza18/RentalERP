using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalCheckIn : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public long RentalCheckOutId { get; private set; }
    public string CheckInNo { get; private set; } = string.Empty;
    public DateTime CheckInDate { get; private set; }
    public long ReceivedBy { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
    public RentalCheckOut? RentalCheckOut { get; private set; }
}
