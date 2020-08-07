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
    public class RoleRepository : IRoleRepository
    {
        private readonly Context _context;
        public RoleRepository(Context context)
        {
            _context = context;
        }

        public Task<IEnumerable<Role>> Get()
        {
            var query = _context.Roles
                .Include(role => role.Employees)
                .AsNoTracking()
                .AsEnumerable();

            return Task.FromResult(query);
        }

        public async Task<int> Create(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync(default);

            return role.Id;
        }

        public async Task Update(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(default);
        }

        public async Task<bool> Delete(int roleId)
        {
            var entity = await _context.Roles.FindAsync(roleId);
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
