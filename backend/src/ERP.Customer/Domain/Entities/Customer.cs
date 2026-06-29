using ERP.SharedKernel.Base;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Customer.Domain.Entities;

public sealed class Customer : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string CustomerCode { get; private set; } = string.Empty;
    public string CustomerName { get; private set; } = string.Empty;
    public string? LegalName { get; private set; }
    public long CustomerGroupId { get; private set; }
    public long CustomerCategoryId { get; private set; }
    public long? CustomerIndustryId { get; private set; }
    public long? CustomerTerritoryId { get; private set; }
    public long CustomerPriceLevelId { get; private set; }
    public long CustomerPaymentProfileId { get; private set; }
    public long CustomerCreditProfileId { get; private set; }
    public long? TaxConfigurationId { get; private set; }
    public DateOnly RegistrationDate { get; private set; }
    public short StatusId { get; private set; }
    public string? Remarks { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public TaxConfiguration? TaxConfiguration { get; private set; }
    public CustomerGroup? CustomerGroup { get; private set; }
    public CustomerCategory? CustomerCategory { get; private set; }
    public CustomerIndustry? CustomerIndustry { get; private set; }
    public CustomerTerritory? CustomerTerritory { get; private set; }
    public CustomerPriceLevel? CustomerPriceLevel { get; private set; }
    public CustomerPaymentProfile? CustomerPaymentProfile { get; private set; }
    public CustomerCreditProfile? CustomerCreditProfile { get; private set; }
    public ICollection<CustomerAddress> CustomerAddresses { get; private set; } = [];
    public ICollection<CustomerContact> CustomerContacts { get; private set; } = [];
    public ICollection<CustomerAttachment> CustomerAttachments { get; private set; } = [];
    public ICollection<CustomerNote> CustomerNotes { get; private set; } = [];
    public ICollection<CustomerActivity> CustomerActivities { get; private set; } = [];
    public ICollection<CustomerTimeline> CustomerTimelines { get; private set; } = [];
}
