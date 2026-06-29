using ERP.SharedKernel.Base;

namespace ERP.Audit.Domain.Entities;

public sealed class ApiAudit : AuditableEntity
{
    public long? UserId { get; private set; }
    public string RequestMethod { get; private set; } = string.Empty;
    public string RequestUrl { get; private set; } = string.Empty;
    public string? RequestBody { get; private set; }
    public int ResponseCode { get; private set; }
    public string? ResponseBody { get; private set; }
    public int ExecutionTimeMs { get; private set; }
    public string? ClientIPAddress { get; private set; }
    public Guid? CorrelationId { get; private set; }
    public DateTime RequestDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
