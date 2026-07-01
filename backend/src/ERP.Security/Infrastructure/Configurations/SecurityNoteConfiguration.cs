using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ERP.Security.Domain.Entities;

namespace ERP.Security.Infrastructure.Configurations;

public sealed class SecurityNoteConfiguration : IEntityTypeConfiguration<SecurityNote>
{
    public void Configure(EntityTypeBuilder<SecurityNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();
        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.HasOne(x => x.SecurityPolicy)
            .WithMany(x => x.SecurityNotes)
            .HasForeignKey(x => x.SecurityPolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.SecurityPolicyId, x.NoteId })
            .IsUnique()
            .HasDatabaseName("uq_security_note_policy_note");

        builder.HasIndex(x => x.SecurityPolicyId)
            .HasDatabaseName("ix_security_note_security_policy_id");

        builder.HasIndex(x => x.NoteId)
            .HasDatabaseName("ix_security_note_note_id");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
