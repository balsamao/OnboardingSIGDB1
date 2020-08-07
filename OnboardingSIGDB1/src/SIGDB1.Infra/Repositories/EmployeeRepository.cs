using Microsoft.EntityFrameworkCore;
using SIGDB1.Core.Extensions;
using SIGDB1.Domain.Entities;
using SIGDB1.Domain.Repositories;
using SIGDB1.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGDB1.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;
        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public Task<IEnumerable<Employee>> Get()
        {
            var query = _context.Employees
                .Include(employee => employee.Company)
                .Include(employee => employee.Role)
                .AsNoTracking()
                .AsEnumerable();

            return Task.FromResult(query);
        }

        public async Task<int> Create(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(default);

            return employee.Id;
        }

        public async Task Update(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(default);
        }

        public async Task<bool> Delete(int employeeId)
        {
            var entity = await _context.Employees.FindAsync(employeeId);
            if (entity.IsNull())
                return false;

            _context.Remove(entity);
            await _context.SaveChangesAsync(default);

            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
