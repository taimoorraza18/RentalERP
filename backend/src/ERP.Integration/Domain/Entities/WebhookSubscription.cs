using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class WebhookSubscription : AuditableEntity
{
    public long IntegrationEndpointId { get; private set; }
    public string EventName { get; private set; } = string.Empty;
    public string WebhookUrl { get; private set; } = string.Empty;
    public string? SecretKey { get; private set; }
    public string HttpMethod { get; private set; } = string.Empty;
    public short RetryCount { get; private set; }
    public int TimeoutSeconds { get; private set; }
    public bool IsEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationEndpoint? IntegrationEndpoint { get; private set; }
}
