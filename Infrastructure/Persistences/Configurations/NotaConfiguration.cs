using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Configurations
{
    public class NotaConfiguration : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(n => n.Valor)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.HasOne(n => n.Estudiante)
                .WithMany(e => e.Notas)
                .HasForeignKey(n => n.IdEstudiante)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Profesor)
                .WithMany(p => p.Notas)
                .HasForeignKey(n => n.IdProfesor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
