namespace ERP.SharedKernel.Interfaces;

public interface ICurrentUser
{
    long UserId { get; }
    string UserName { get; }
    string Email { get; }
    IReadOnlyCollection<string> Roles { get; }
    bool IsAuthenticated { get; }
}
