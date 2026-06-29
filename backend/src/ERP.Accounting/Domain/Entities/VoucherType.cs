using ERP.SharedKernel.Base;
using ERP.Foundation.Domain.Entities;

namespace ERP.Accounting.Domain.Entities;

public sealed class VoucherType : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string VoucherCode { get; private set; } = string.Empty;
    public string VoucherName { get; private set; } = string.Empty;
    public short VoucherCategory { get; private set; }
    public bool AutoNumbering { get; private set; }
    public string? NumberPrefix { get; private set; }
    public int NextNumber { get; private set; }
    public bool IsSystemDefined { get; private set; }
    public bool IsActive { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
}
