using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationConnection : AuditableEntity
{
    public long IntegrationEndpointId { get; private set; }
    public string ConnectionName { get; private set; } = string.Empty;
    public short AuthenticationType { get; private set; }
    public string? ClientId { get; private set; }
    public string? ClientSecret { get; private set; }
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public string? CertificateThumbprint { get; private set; }
    public DateTime? TokenExpiryDate { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationEndpoint? IntegrationEndpoint { get; private set; }
}
