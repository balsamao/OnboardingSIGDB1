using SIGDB1.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGDB1.Application.Services
{
    public interface IRoleService
    {
        List<string> Errors { get; }

        bool Success();

        Task<List<GetRoleDto>> Filter(FilterRoleDto filter);

        Task<GetRoleDto> Create(CreateRoleDto dto);

        Task<GetRoleDto> Update(int id, UpdateRoleDto dto);

        Task<bool> Delete(int id);
    }
}
