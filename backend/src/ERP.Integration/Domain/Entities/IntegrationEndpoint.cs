using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationEndpoint : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string EndpointCode { get; private set; } = string.Empty;
    public string EndpointName { get; private set; } = string.Empty;
    public short EndpointType { get; private set; }
    public string BaseUrl { get; private set; } = string.Empty;
    public short AuthenticationType { get; private set; }
    public int TimeoutSeconds { get; private set; }
    public bool IsEnabled { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<IntegrationConnection> IntegrationConnections { get; private set; } = [];
    public ICollection<WebhookSubscription> WebhookSubscriptions { get; private set; } = [];
    public ICollection<IntegrationJob> IntegrationJobs { get; private set; } = [];
    public ICollection<IntegrationHealth> IntegrationHealths { get; private set; } = [];
    public ICollection<IntegrationAttachment> IntegrationAttachments { get; private set; } = [];
    public ICollection<IntegrationNote> IntegrationNotes { get; private set; } = [];
    public ICollection<IntegrationActivity> IntegrationActivities { get; private set; } = [];
    public ICollection<IntegrationTimeline> IntegrationTimelines { get; private set; } = [];
}
