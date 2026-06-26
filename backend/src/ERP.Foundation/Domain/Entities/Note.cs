using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Note : AuditableEntity
{
    public string? Title { get; private set; }
    public string NoteText { get; private set; } = string.Empty;
    public string NoteType { get; private set; } = "General";
    public short Priority { get; private set; }
    public bool IsPinned { get; private set; }
    public bool IsConfidential { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
