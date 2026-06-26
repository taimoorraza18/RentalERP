using System.Text.RegularExpressions;

namespace ERP.SharedKernel.Guards;

public static partial class Guard
{
    public static T NotNull<T>(T? value, string paramName) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
        return value;
    }

    public static string NotEmpty(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be null or empty.", paramName);
        return value;
    }

    public static T Positive<T>(T value, string paramName) where T : struct, IComparable<T>
    {
        if (value.CompareTo(default) <= 0)
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be a positive value.");
        return value;
    }

    public static string ValidEmail(string? email, string paramName)
    {
        NotEmpty(email, paramName);
        if (!EmailRegex().IsMatch(email!))
            throw new ArgumentException($"'{email}' is not a valid email address.", paramName);
        return email!;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
}
