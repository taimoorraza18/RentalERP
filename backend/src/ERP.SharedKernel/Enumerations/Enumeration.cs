using System.Reflection;

namespace ERP.SharedKernel.Enumerations;

public abstract class Enumeration(int id, string name) : IComparable<Enumeration>
{
    public int Id { get; } = id;
    public string Name { get; } = name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(T))
            .Select(f => f.GetValue(null))
            .Cast<T>();

    public static T? FromId<T>(int id) where T : Enumeration =>
        GetAll<T>().FirstOrDefault(e => e.Id == id);

    public static T? FromName<T>(string name) where T : Enumeration =>
        GetAll<T>().FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    public override string ToString() => Name;

    public override bool Equals(object? obj) =>
        obj is Enumeration other && GetType() == other.GetType() && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(Enumeration? other) => other is null ? 1 : Id.CompareTo(other.Id);

    public static bool operator ==(Enumeration? left, Enumeration? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Enumeration? left, Enumeration? right) =>
        !(left == right);
}
