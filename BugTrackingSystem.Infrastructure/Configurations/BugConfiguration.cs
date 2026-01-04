using BugTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTrackingSystem.Infrastructure.Configurations
{
    public class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.ToTable("Bugs");

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Description)
                .HasMaxLength(2000);

            builder.HasOne(b => b.Reporter)
                .WithMany()
                .HasForeignKey(b => b.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.AssignedTo)
                .WithMany()
                .HasForeignKey(b => b.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Attachments)
                .WithOne(a => a.Bug)
                .HasForeignKey(a => a.BugId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
