namespace ERP.SharedKernel.Common;

public sealed class SortDefinition
{
    public string Field { get; }
    public SortDirection Direction { get; }

    private SortDefinition(string field, SortDirection direction)
    {
        Field = field;
        Direction = direction;
    }

    public static SortDefinition Ascending(string field) =>
        new(field, SortDirection.Ascending);

    public static SortDefinition Descending(string field) =>
        new(field, SortDirection.Descending);
}
