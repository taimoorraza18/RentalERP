using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalCheckOut : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public string CheckOutNo { get; private set; } = string.Empty;
    public DateTime CheckOutDate { get; private set; }
    public long ReleasedBy { get; private set; }
    public string? CustomerRepresentative { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
}
