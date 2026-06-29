
-----------------------------------------------------------------------
1. Namespace Aliases
-----------------------------------------------------------------------

Do not use aliases ending with "Ref".

Use aliases ending with "Entity".

-----------------------------------------------------------------------
2. FiscalYear
-----------------------------------------------------------------------

Remove FiscalYear from ERP.Foundation.

FiscalYear belongs exclusively to ERP.Accounting.

Update all references accordingly.

-----------------------------------------------------------------------
3. AssetGroup
-----------------------------------------------------------------------

Add AssetGroupId to Asset.

Relationship

AssetGroup (1) -> (N) Asset

-----------------------------------------------------------------------
4. AssetStatus
-----------------------------------------------------------------------

Remove the AssetStatus entity completely.

Asset.CurrentStatus remains a SMALLINT enum.

Do not generate AssetStatus table.

Remove all related configurations and navigation properties.

-----------------------------------------------------------------------
5. Employee References
-----------------------------------------------------------------------

The Employee / HR module does not yet exist.

The following properties remain FK values only.

Do NOT generate navigation properties.

- InspectorId
- ApprovedBy
- PostedBy
- TechnicianId
- RequestedBy
- AssignedTo
- CompletedBy
- ReceivedBy
- DeliveredBy
- ReleasedBy

All remain long / long?.

-----------------------------------------------------------------------
6. Service Reference
-----------------------------------------------------------------------

AssetMaintenance.ServiceId remains a FK property only.

Do not generate a navigation property.

This avoids circular dependencies.

-----------------------------------------------------------------------
7. Department
-----------------------------------------------------------------------

DO NOT Create Department entity.

Change PurchaseRequisition.Department to NVARCHAR(150) NULLABLE

-----------------------------------------------------------------------
8. Bank Accounts
-----------------------------------------------------------------------

Do NOT create a new Account or BankAccount entity.

The existing Accounting.ChartOfAccount entity is the single source of truth for all ledger accounts in the ERP.

Reuse ChartOfAccount everywhere.

Update all foreign keys to reference ChartOfAccountId.

i.e.

CustomerPayment
    DepositAccountId -> ChartOfAccountId

InventoryAdjustment
    AdjustmentAccountId -> ChartOfAccountId

PurchaseOrderLine
    ExpenseAccountId -> ChartOfAccountId

Item
    SalesAccountId -> ChartOfAccountId
    PurchaseAccountId -> ChartOfAccountId

JournalEntryLine
    ChartOfAccountId -> ChartOfAccountId

Do not introduce duplicate account entities.

-----------------------------------------------------------------------
9. Tax
-----------------------------------------------------------------------

Remove TaxProfile completely.

Replace every reference with TaxConfigurationId as FK property

Entity: TaxConfiguration will be coming in future prompt

-----------------------------------------------------------------------
10. CostCenter / Project
-----------------------------------------------------------------------

For Version 1

Keep

CostCenterId

ProjectId

as FK properties only.

Do not generate entities.

Do not generate navigation properties.

These entities will be implemented in a future phase.

-----------------------------------------------------------------------
11. Unit
-----------------------------------------------------------------------

Rename ItemUnit to Unit

Unit is a shared master entity.

Update all references, navigation properties, EF configurations and foreign keys accordingly.

-----------------------------------------------------------------------
12. General Rules
-----------------------------------------------------------------------

Every FK should have a navigation property ONLY if the referenced entity currently exists.

If the referenced entity has not yet been implemented, keep only the FK property.

Do not generate placeholder entities.

Do not invent missing domains.

Do not duplicate entities across modules.