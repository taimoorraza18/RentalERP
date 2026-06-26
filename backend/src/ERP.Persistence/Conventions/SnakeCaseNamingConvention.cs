using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.RegularExpressions;

namespace ERP.Persistence.Conventions;

public sealed partial class SnakeCaseNamingConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            if (entityType.GetTableName() is { } tableName)
                entityType.SetTableName(ToSnakeCase(tableName));

            foreach (var property in entityType.GetProperties())
            {
                var table = entityType.GetTableName();
                if (table is null) continue;

                var storeId = StoreObjectIdentifier.Table(table, entityType.GetSchema());
                var columnName = property.GetColumnName(storeId);
                if (columnName is not null)
                    property.SetColumnName(ToSnakeCase(columnName));
            }

            foreach (var key in entityType.GetKeys())
            {
                if (key.GetName() is { } name)
                    key.SetName(ToSnakeCase(name));
            }

            foreach (var fk in entityType.GetForeignKeys())
            {
                if (fk.GetConstraintName() is { } name)
                    fk.SetConstraintName(ToSnakeCase(name));
            }

            foreach (var index in entityType.GetIndexes())
            {
                if (index.GetDatabaseName() is { } name)
                    index.SetDatabaseName(ToSnakeCase(name));
            }
        }
    }

    [GeneratedRegex(@"([A-Z]+)([A-Z][a-z])")]
    private static partial Regex UpperUpperLower();

    [GeneratedRegex(@"([a-z\d])([A-Z])")]
    private static partial Regex LowerUpper();

    private static string ToSnakeCase(string name)
    {
        var result = UpperUpperLower().Replace(name, "$1_$2");
        result = LowerUpper().Replace(result, "$1_$2");
        return result.ToLowerInvariant();
    }
}
