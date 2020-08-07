using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGDB1.Domain.Entities;

namespace SIGDB1.Infra.Data.Mapping
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Name)
                .HasColumnName("Nome")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(p => p.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("nvarchar(14)")
                .IsRequired();

            builder.Property(p => p.Fundation)
                .HasColumnName("DataFundacao")
                .HasColumnType("datetime2(7)")
                .IsRequired(false);

            builder.ToTable("Empresas");
        }
    }
}
