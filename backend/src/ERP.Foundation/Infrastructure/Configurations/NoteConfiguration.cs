using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Foundation.Domain.Entities;

namespace ERP.Foundation.Infrastructure.Configurations;

public sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Property(x => x.Title).HasMaxLength(200);
        builder.Property(x => x.NoteText).IsRequired();
        builder.Property(x => x.NoteType).IsRequired().HasMaxLength(30).HasDefaultValue("General");
        builder.Property(x => x.IsPinned).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsConfidential).IsRequired().HasDefaultValue(false);

        builder.HasIndex(x => x.NoteType)
            .HasDatabaseName("ix_note_note_type");

        builder.HasIndex(x => x.IsPinned)
            .HasDatabaseName("ix_note_is_pinned");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
