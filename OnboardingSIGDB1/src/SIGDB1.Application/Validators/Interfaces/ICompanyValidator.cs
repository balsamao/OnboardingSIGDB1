using FluentValidation.Results;
using SIGDB1.Application.Dtos;
using System.Threading.Tasks;

namespace SIGDB1.Application.Validators
{
    public interface ICompanyValidator
    {
        Task<ValidationResult> CreateValidate(CreateCompanyDto dto);

        Task<ValidationResult> UpdateValidate(int id, UpdateCompanyDto dto);
    }
}
