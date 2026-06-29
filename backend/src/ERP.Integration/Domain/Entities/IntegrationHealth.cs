using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationHealth : AuditableEntity
{
    public long IntegrationEndpointId { get; private set; }
    public DateTime HealthCheckDate { get; private set; }
    public int ResponseTimeMs { get; private set; }
    public bool IsHealthy { get; private set; }
    public int? HttpStatusCode { get; private set; }
    public string? FailureReason { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationEndpoint? IntegrationEndpoint { get; private set; }
}
