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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Context _context;
        public CompanyRepository(Context context)
        {
            _context = context;
        }

        public Task<IEnumerable<Company>> Get()
        {
            var query =  _context.Companies
                .Include(company => company.Employees)
                .AsNoTracking()
                .AsEnumerable();

            return Task.FromResult(query);
        }

        public async Task<int> Create(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync(default);

            return company.Id;
        }

        public async Task Update(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync(default);
        }

        public async Task<bool> Delete(int companyId)
        {
            var entity = await _context.Companies.FindAsync(companyId);
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
