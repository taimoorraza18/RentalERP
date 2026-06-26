using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class User : AggregateRoot
{
    public long CompanyId { get; private set; }
    public long? DefaultBranchId { get; private set; }
    public long? EmployeeId { get; private set; }
    public string UserCode { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string? PasswordSalt { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string? LastName { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? Mobile { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public bool IsLocked { get; private set; }
    public bool IsSystemAdmin { get; private set; }
    public bool IsActive { get; private set; }
    public byte[] RowVersion { get; private set; } = [];

    public Company? Company { get; private set; }
    public Branch? DefaultBranch { get; private set; }
    public ICollection<UserRole> UserRoles { get; private set; } = [];
}
