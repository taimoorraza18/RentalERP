namespace ERP.SharedKernel.Common;

public sealed class FilterCriteria
{
    private readonly List<FilterEntry> _entries = [];

    public IReadOnlyList<FilterEntry> Entries => _entries.AsReadOnly();

    public bool HasEntries => _entries.Count > 0;

    public FilterCriteria Add(string field, string @operator, object? value)
    {
        _entries.Add(new FilterEntry(field, @operator, value));
        return this;
    }

    public sealed record FilterEntry(string Field, string Operator, object? Value);
}
