using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalTimeline : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public long TimelineId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
}
