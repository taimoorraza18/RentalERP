namespace ERP.SharedKernel.Exceptions;

public sealed class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string resource, object id)
        : base($"{resource} with id '{id}' was not found.") { }
}
