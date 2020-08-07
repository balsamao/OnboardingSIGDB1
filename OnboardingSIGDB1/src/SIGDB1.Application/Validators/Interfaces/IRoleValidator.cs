using FluentValidation.Results;
using SIGDB1.Application.Dtos;
using System.Threading.Tasks;

namespace SIGDB1.Application.Validators
{
    public interface IRoleValidator
    {
        Task<ValidationResult> CreateValidate(CreateRoleDto dto);

        Task<ValidationResult> UpdateValidate(int id, UpdateRoleDto dto);
    }
}
