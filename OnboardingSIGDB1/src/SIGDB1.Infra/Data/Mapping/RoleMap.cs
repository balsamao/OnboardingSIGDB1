using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGDB1.Domain.Entities;

namespace SIGDB1.Infra.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Description)
                .HasColumnName("Descricao")
                .HasColumnType("nvarchar(250)")
                .IsRequired();

            builder.ToTable("Cargos");
        }
    }
}
