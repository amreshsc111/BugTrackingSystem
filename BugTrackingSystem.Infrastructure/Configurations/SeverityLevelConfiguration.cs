using BugTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTrackingSystem.Infrastructure.Configurations
{
    public class BugSeverityConfiguration : IEntityTypeConfiguration<SeverityLevel>
    {
        public void Configure(EntityTypeBuilder<SeverityLevel> builder)
        {
            builder.ToTable("SeverityLevels");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Description)
                .HasMaxLength(250);
        }
    }
}
