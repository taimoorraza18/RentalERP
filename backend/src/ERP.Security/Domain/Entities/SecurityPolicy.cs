using ERP.SharedKernel.Base;

namespace ERP.Security.Domain.Entities;

public sealed class SecurityPolicy : AuditableEntity
{
    public long CompanyId { get; private set; }
    public string PolicyName { get; private set; } = string.Empty;
    public short MinimumPasswordLength { get; private set; }
    public bool RequireUpperCase { get; private set; }
    public bool RequireLowerCase { get; private set; }
    public bool RequireNumeric { get; private set; }
    public bool RequireSpecialCharacter { get; private set; }
    public int PasswordExpiryDays { get; private set; }
    public short MaxLoginAttempts { get; private set; }
    public int SessionTimeoutMinutes { get; private set; }
    public bool RequireMfa { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public ICollection<UserSession> UserSessions { get; private set; } = [];
    public ICollection<SecurityAttachment> SecurityAttachments { get; private set; } = [];
    public ICollection<SecurityNote> SecurityNotes { get; private set; } = [];
    public ICollection<SecurityActivity> SecurityActivities { get; private set; } = [];
    public ICollection<SecurityTimeline> SecurityTimelines { get; private set; } = [];
}
