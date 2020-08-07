using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SIGDB1.Application.Services;
using SIGDB1.Application.Validators;
using System.Reflection;

namespace SIGDB1.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoleValidator, RoleValidator>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICompanyValidator, CompanyValidator>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeValidator, EmployeeValidator>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
