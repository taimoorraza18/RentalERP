namespace ERP.SharedKernel.Exceptions;

public sealed class BusinessRuleException : DomainException
{
    public string Rule { get; }

    public BusinessRuleException(string rule, string message) : base(message)
    {
        Rule = rule;
    }
}
