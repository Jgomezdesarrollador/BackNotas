using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Configurations
{
    public class LogErrorConfiguration : IEntityTypeConfiguration<LogError>
    {
        public void Configure(EntityTypeBuilder<LogError> builder)
        {
            builder.ToTable("LogErrores");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nivel)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Mensaje)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Origen)
                .HasMaxLength(100);

            builder.Property(e => e.Fecha)
                .IsRequired();
        }
    }
}
