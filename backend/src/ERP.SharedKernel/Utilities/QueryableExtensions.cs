using ERP.SharedKernel.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace ERP.SharedKernel.Utilities;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPaging<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        pageNumber = Math.Max(1, pageNumber);
        pageSize = Math.Clamp(pageSize, 1, 100);
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string? sortBy,
        SortDirection direction = SortDirection.Ascending)
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            return query;

        var property = typeof(T).GetProperty(
            sortBy,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (property is null)
            return query;

        var param = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(param, property);
        var selector = Expression.Lambda(propertyAccess, param);

        var methodName = direction == SortDirection.Descending
            ? nameof(Queryable.OrderByDescending)
            : nameof(Queryable.OrderBy);

        var sortExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            [typeof(T), property.PropertyType],
            query.Expression,
            Expression.Quote(selector));

        return query.Provider.CreateQuery<T>(sortExpression);
    }

    public static IQueryable<T> ApplySearch<T>(
        this IQueryable<T> query,
        string? search,
        Expression<Func<T, bool>> predicate)
    {
        return string.IsNullOrWhiteSpace(search) ? query : query.Where(predicate);
    }
}
