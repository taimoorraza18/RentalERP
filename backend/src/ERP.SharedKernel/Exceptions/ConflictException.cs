namespace ERP.SharedKernel.Exceptions;

public sealed class ConflictException : DomainException
{
    public ConflictException(string message) : base(message) { }

    public ConflictException(string resource, string message)
        : base($"{resource}: {message}") { }
}
