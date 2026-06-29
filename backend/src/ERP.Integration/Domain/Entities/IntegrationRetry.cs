using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationRetry : AuditableEntity
{
    public long IntegrationJobId { get; private set; }
    public long IntegrationMessageId { get; private set; }
    public short RetryNumber { get; private set; }
    public DateTime RetryDate { get; private set; }
    public string? RetryReason { get; private set; }
    public short RetryStatus { get; private set; }
    public int? ResponseCode { get; private set; }
    public string? ErrorMessage { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationJob? IntegrationJob { get; private set; }
    public IntegrationMessage? IntegrationMessage { get; private set; }
}
