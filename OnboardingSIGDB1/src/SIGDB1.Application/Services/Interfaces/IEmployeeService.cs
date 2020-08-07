using SIGDB1.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGDB1.Application.Services
{
    public interface IEmployeeService
    {
        List<string> Errors { get; }

        bool Success();

        Task<List<GetEmployeeDto>> Filter(FilterEmployeeDto filter);

        Task<GetEmployeeDto> Create(CreateEmployeeDto dto);

        Task<GetEmployeeDto> Update(int id, UpdateEmployeeDto dto);

        Task<bool> Delete(int id);
    }
}
