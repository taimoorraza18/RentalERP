using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;
using CustomerEntity = ERP.Customer.Domain.Entities.Customer;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalReservation : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string ReservationNo { get; private set; } = string.Empty;
    public long? RentalQuotationId { get; private set; }
    public long CustomerId { get; private set; }
    public DateOnly ReservationDate { get; private set; }
    public DateOnly RentalStartDate { get; private set; }
    public DateOnly RentalEndDate { get; private set; }
    public DateOnly ExpiryDate { get; private set; }
    public string? Remarks { get; private set; }
    public bool IsConfirmed { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? Branch { get; private set; }
    public RentalQuotation? RentalQuotation { get; private set; }
    public CustomerEntity? Customer { get; private set; }
    public ICollection<RentalReservationLine> RentalReservationLines { get; private set; } = [];
}
