using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalAttachment : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public long AttachmentId { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
}
