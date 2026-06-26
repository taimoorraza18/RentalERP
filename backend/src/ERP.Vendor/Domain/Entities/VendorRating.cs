using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class VendorRating : AuditableEntity
{
    public string RatingCode { get; private set; } = string.Empty;
    public string RatingName { get; private set; } = string.Empty;
    public decimal MinimumScore { get; private set; }
    public decimal MaximumScore { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<Vendor> Vendors { get; private set; } = [];
}
