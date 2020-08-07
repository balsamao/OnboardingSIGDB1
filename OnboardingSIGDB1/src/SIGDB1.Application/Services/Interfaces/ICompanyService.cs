using SIGDB1.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGDB1.Application.Services
{
    public interface ICompanyService 
    {
        List<string> Errors { get; }

        bool Success();

        Task<List<GetCompanyDto>> Filter(FilterCompanyDto filter);

        Task<GetCompanyDto> Create(CreateCompanyDto dto);

        Task<GetCompanyDto> Update(int id, UpdateCompanyDto dto);

        Task<bool> Delete(int id);
    }
}
