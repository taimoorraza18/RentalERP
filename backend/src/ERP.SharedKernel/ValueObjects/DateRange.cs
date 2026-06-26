using ERP.SharedKernel.Abstractions;

namespace ERP.SharedKernel.ValueObjects;

public sealed class DateRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }
    public TimeSpan Duration => End - Start;

    private DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static DateRange Of(DateTime start, DateTime end)
    {
        if (end < start)
            throw new ArgumentException("End date must be on or after start date.");
        return new DateRange(start, end);
    }

    public static DateRange SingleDay(DateTime date) =>
        new(date.Date, date.Date.AddDays(1).AddTicks(-1));

    public bool Contains(DateTime date) => date >= Start && date <= End;

    public bool Overlaps(DateRange other) => Start < other.End && End > other.Start;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }

    public override string ToString() => $"{Start:yyyy-MM-dd} to {End:yyyy-MM-dd}";
}
