using ERP.SharedKernel.Base;

namespace ERP.Vendor.Domain.Entities;

public sealed class Vendor : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long BranchId { get; private set; }
    public string VendorCode { get; private set; } = string.Empty;
    public string VendorName { get; private set; } = string.Empty;
    public string? DisplayName { get; private set; }
    public long VendorGroupId { get; private set; }
    public long VendorCategoryId { get; private set; }
    public long? VendorIndustryId { get; private set; }
    public long? VendorTerritoryId { get; private set; }
    public long? VendorPaymentProfileId { get; private set; }
    public long? VendorRatingId { get; private set; }
    public long? VendorTaxProfileId { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Mobile { get; private set; }
    public string? Website { get; private set; }
    public string? NTN { get; private set; }
    public string? STRN { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public VendorGroup? VendorGroup { get; private set; }
    public VendorCategory? VendorCategory { get; private set; }
    public VendorIndustry? VendorIndustry { get; private set; }
    public VendorTerritory? VendorTerritory { get; private set; }
    public VendorPaymentProfile? VendorPaymentProfile { get; private set; }
    public VendorRating? VendorRating { get; private set; }
    public VendorTaxProfile? VendorTaxProfile { get; private set; }
    public ICollection<VendorAddress> VendorAddresses { get; private set; } = [];
    public ICollection<VendorContact> VendorContacts { get; private set; } = [];
    public ICollection<VendorAttachment> VendorAttachments { get; private set; } = [];
    public ICollection<VendorNote> VendorNotes { get; private set; } = [];
    public ICollection<VendorActivity> VendorActivities { get; private set; } = [];
    public ICollection<VendorTimeline> VendorTimelines { get; private set; } = [];
}
