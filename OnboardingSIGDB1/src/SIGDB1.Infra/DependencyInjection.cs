using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGDB1.Domain.Repositories;
using SIGDB1.Infra.Data;
using SIGDB1.Infra.Repositories;

namespace SIGDB1.Infra
{
    public static class DependencyInjection
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("ContextDB")));

            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        }
    }
}
