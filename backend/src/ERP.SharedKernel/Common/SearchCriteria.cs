namespace ERP.SharedKernel.Common;

public sealed class SearchCriteria
{
    public string Term { get; }
    public IReadOnlyList<string> Fields { get; }

    private SearchCriteria(string term, IReadOnlyList<string> fields)
    {
        Term = term;
        Fields = fields;
    }

    public static SearchCriteria For(string term, params string[] fields) =>
        new(term ?? string.Empty, fields);

    public bool HasTerm => !string.IsNullOrWhiteSpace(Term);
    public bool HasFields => Fields.Count > 0;
}
