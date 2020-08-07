using FluentValidation.Results;
using SIGDB1.Application.Dtos;
using System.Threading.Tasks;

namespace SIGDB1.Application.Validators
{
    public interface IEmployeeValidator
    {
        Task<ValidationResult> CreateValidate(CreateEmployeeDto dto);

        Task<ValidationResult> UpdateValidate(int id, UpdateEmployeeDto dto);
    }
}
