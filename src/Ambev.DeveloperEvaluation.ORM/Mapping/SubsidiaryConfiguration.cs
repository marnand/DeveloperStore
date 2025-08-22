using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Entity Framework configuration for Subsidiary entity
/// </summary>
public class SubsidiaryConfiguration : IEntityTypeConfiguration<Subsidiary>
{
    public void Configure(EntityTypeBuilder<Subsidiary> builder)
    {
        builder.ToTable("Subsidiaries");

        // Primary key
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        // Properties
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.CreatedAt)
            .IsRequired();

        builder.Property(s => s.UpdatedAt);

        // Indexes
        builder.HasIndex(s => s.Name);
    }
}