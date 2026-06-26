using ERP.SharedKernel.Base;

namespace ERP.Foundation.Domain.Entities;

public sealed class Contact : AuditableEntity
{
    public string? Title { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; }
    public string? LastName { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public string? Designation { get; private set; }
    public string? Department { get; private set; }
    public string? Email { get; private set; }
    public string? MobileNo { get; private set; }
    public string? PhoneNo { get; private set; }
    public string? Extension { get; private set; }
    public string? WhatsAppNo { get; private set; }
    public string PreferredContactMethod { get; private set; } = "Email";
    public string? Remarks { get; private set; }
    public short StatusId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
}
