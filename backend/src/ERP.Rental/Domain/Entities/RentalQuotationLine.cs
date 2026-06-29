using ERP.SharedKernel.Base;
using AssetEntity = ERP.Asset.Domain.Entities.Asset;
using TaxConfiguration = ERP.SystemConfiguration.Domain.Entities.TaxConfiguration;

namespace ERP.Rental.Domain.Entities;

public sealed class RentalQuotationLine : AuditableEntity
{
    public long RentalQuotationId { get; private set; }
    public long AssetId { get; private set; }
    public int RentalDays { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitRate { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public long? TaxConfigurationId { get; private set; }
    public decimal TaxAmount { get; private set; }
    public decimal LineTotal { get; private set; }
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public RentalQuotation? RentalQuotation { get; private set; }
    public TaxConfiguration? TaxConfiguration { get; private set; }
    public AssetEntity? Asset { get; private set; }
}
