using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationMessage : AuditableEntity
{
    public long IntegrationJobId { get; private set; }
    public short Direction { get; private set; }
    public string MessageType { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public Guid? CorrelationId { get; private set; }
    public DateTime? SentDate { get; private set; }
    public DateTime? ReceivedDate { get; private set; }
    public short ProcessingStatus { get; private set; }
    public string? ErrorMessage { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationJob? IntegrationJob { get; private set; }
    public ICollection<IntegrationRetry> IntegrationRetries { get; private set; } = [];
}
