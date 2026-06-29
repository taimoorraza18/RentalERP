using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalAgreement : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string AgreementNo { get; private set; } = string.Empty;
    public long? RentalReservationId { get; private set; }
    public long CustomerId { get; private set; }
    public DateOnly AgreementDate { get; private set; }
    public DateOnly RentalStartDate { get; private set; }
    public DateOnly RentalEndDate { get; private set; }
    public decimal SecurityDeposit { get; private set; }
    public decimal TotalRentalAmount { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsPosted { get; private set; }
    public DateTime? PostedDate { get; private set; }
    public long? PostedBy { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public RentalReservation? RentalReservation { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public ICollection<RentalAgreementLine> RentalAgreementLines { get; private set; } = [];
    public ICollection<RentalCheckOut> RentalCheckOuts { get; private set; } = [];
    public ICollection<RentalCheckIn> RentalCheckIns { get; private set; } = [];
    public ICollection<RentalReturn> RentalReturns { get; private set; } = [];
    public ICollection<RentalExtension> RentalExtensions { get; private set; } = [];
    public ICollection<RentalInvoice> RentalInvoices { get; private set; } = [];
    public ICollection<RentalAttachment> RentalAttachments { get; private set; } = [];
    public ICollection<RentalNote> RentalNotes { get; private set; } = [];
    public ICollection<RentalActivity> RentalActivities { get; private set; } = [];
    public ICollection<RentalTimeline> RentalTimelines { get; private set; } = [];
}
