using ERP.SharedKernel.Base;

namespace ERP.Scheduler.Domain.Entities;

public sealed class JobParameter : AuditableEntity
{
    public long ScheduledJobId { get; private set; }
    public string ParameterName { get; private set; } = string.Empty;
    public string? ParameterValue { get; private set; }
    public string DataType { get; private set; } = string.Empty;
    public bool IsEncrypted { get; private set; }
    public int DisplayOrder { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ScheduledJob? ScheduledJob { get; private set; }
}
