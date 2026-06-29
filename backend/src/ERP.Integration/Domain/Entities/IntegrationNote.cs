using ERP.SharedKernel.Base;

namespace ERP.Integration.Domain.Entities;

public sealed class IntegrationNote : AuditableEntity
{
    public long IntegrationEndpointId { get; private set; }
    public long NoteId { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public IntegrationEndpoint? IntegrationEndpoint { get; private set; }
}
