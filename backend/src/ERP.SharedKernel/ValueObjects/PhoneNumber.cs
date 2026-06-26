using ERP.SharedKernel.Abstractions;
using System.Text.RegularExpressions;

namespace ERP.SharedKernel.ValueObjects;

public sealed partial class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Of(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber));

        var digits = DigitsOnly().Replace(phoneNumber, string.Empty);

        if (digits.Length < 7 || digits.Length > 15)
            throw new ArgumentException(
                $"'{phoneNumber}' is not a valid phone number. Must have 7–15 digits.", nameof(phoneNumber));

        return new PhoneNumber(phoneNumber.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(PhoneNumber phone) => phone.Value;

    [GeneratedRegex(@"[^\d]")]
    private static partial Regex DigitsOnly();
}
