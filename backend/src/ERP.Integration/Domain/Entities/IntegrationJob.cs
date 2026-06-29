using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationJob : AuditableEntity
{
    public long IntegrationEndpointId { get; private set; }
    public string JobName { get; private set; } = string.Empty;
    public short JobType { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public short JobStatus { get; private set; }
    public int TotalRecords { get; private set; }
    public int ProcessedRecords { get; private set; }
    public int FailedRecords { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationEndpoint? IntegrationEndpoint { get; private set; }
    public ICollection<IntegrationMessage> IntegrationMessages { get; private set; } = [];
    public ICollection<IntegrationRetry> IntegrationRetries { get; private set; } = [];
}
