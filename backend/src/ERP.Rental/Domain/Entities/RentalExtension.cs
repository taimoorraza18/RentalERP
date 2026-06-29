using ERP.SharedKernel.Base;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalExtension : AuditableEntity
{
    public long RentalAgreementId { get; private set; }
    public string ExtensionNo { get; private set; } = string.Empty;
    public DateOnly ExtensionDate { get; private set; }
    public DateOnly PreviousEndDate { get; private set; }
    public DateOnly NewEndDate { get; private set; }
    public int AdditionalDays { get; private set; }
    public decimal AdditionalAmount { get; private set; }
    public string? Reason { get; private set; }
    public bool IsApproved { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalAgreement? RentalAgreement { get; private set; }
}
