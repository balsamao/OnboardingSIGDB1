using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIGDB1.Domain.Entities;

namespace SIGDB1.Infra.Data.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Name)
                .HasColumnName("Nome")
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(p => p.Cpf)
                .HasColumnName("Cpf")
                .HasColumnType("nvarchar(11)")
                .IsRequired();

            builder.Property(p => p.Hiring)
                .HasColumnName("DataFundacao")
                .HasColumnType("datetime2(7)")
                .IsRequired(false);

            builder.HasOne(p => p.Company)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Role)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Funcionarios");
        }
    }
}
