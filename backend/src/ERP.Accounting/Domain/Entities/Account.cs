using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class Account : AuditableEntity
{
    public long CompanyId { get; private set; }
    public long? ParentAccountId { get; private set; }
    public string AccountCode { get; private set; } = string.Empty;
    public string AccountName { get; private set; } = string.Empty;
    public short AccountType { get; private set; }
    public short AccountNature { get; private set; }
    public bool IsControlAccount { get; private set; }
    public bool IsDetailAccount { get; private set; }
    public bool IsActive { get; private set; }
    public long? CurrencyId { get; private set; }
    public int Level { get; private set; }
    public int SortOrder { get; private set; }
    public string? Description { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Currency? Currency { get; private set; }
    public Account? ParentAccount { get; private set; }
    public ICollection<Account> ChildAccounts { get; private set; } = [];
}
