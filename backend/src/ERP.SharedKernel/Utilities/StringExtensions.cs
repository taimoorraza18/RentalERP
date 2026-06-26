using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ERP.SharedKernel.Utilities;

public static partial class StringExtensions
{
    public static string ToSlug(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var slug = value.Trim().ToLowerInvariant();
        slug = NonAlphanumericOrSpaceOrDash().Replace(slug, string.Empty);
        slug = MultipleSpaces().Replace(slug, "-");
        slug = MultipleDashes().Replace(slug, "-");
        return slug.Trim('-');
    }

    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    public static string Truncate(this string value, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;

        var truncateAt = Math.Max(0, maxLength - suffix.Length);
        return string.Concat(value.AsSpan(0, truncateAt), suffix);
    }

    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex NonAlphanumericOrSpaceOrDash();

    [GeneratedRegex(@"\s+")]
    private static partial Regex MultipleSpaces();

    [GeneratedRegex(@"-+")]
    private static partial Regex MultipleDashes();
}
