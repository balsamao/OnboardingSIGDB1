using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using SIGDB1.Application.Dtos;
using SIGDB1.Core.Extensions;
using SIGDB1.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGDB1.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>, IEmployeeValidator
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(x => x.Name)
                .Must(name => name.IsEmpty() == false)
                .WithMessage("Name is required.");

            RuleFor(x => x.Cpf)
                .Must(cpf => cpf.OnlyNumbers().IsEmpty() == false)
                .WithMessage("CPF is required.");

            When(x => !x.Cpf.IsEmpty(), () => {
                RuleFor(x => x.Cpf).Must(ValidarCpf).WithMessage("CPF invalid.");
            });
        }

        private bool ValidarCpf(EmployeeDto employeeDto, string cpf, PropertyValidatorContext ctx)
        {
            if (cpf.OnlyNumbers().Length < 11)
                return false;

            if (cpf.IsEquals(cpf.ElementAt(0).ToString().PadLeft(11, cpf.ElementAt(0))))
                return false;

            return true;
        }

        public async Task<ValidationResult> CreateValidate(CreateEmployeeDto employeeDto)
        {
            var validations = Validate(employeeDto);

            if (!validations.IsValid)
                return validations;

            var query = await _employeeRepository.Get();
            if (query.Any(employee => employee.Name.IsEquals(employeeDto.Name)))
                validations.Errors.Add(new ValidationFailure("Error", "Name must be unique."));
            if (query.Any(employee => employee.Cpf.IsEquals(employeeDto.Cpf.OnlyNumbers())))
                validations.Errors.Add(new ValidationFailure("Error", "CPF must be unique."));

            return validations;
        }

        public async Task<ValidationResult> UpdateValidate(int employeeId, UpdateEmployeeDto employeeDto)
        {
            if (employeeId != employeeDto.Id)
                return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Error", "Identification is invalid.") }); 

            var validations = Validate(employeeDto);

            if (!validations.IsValid)
                return validations;

            var query = await _employeeRepository.Get();

            if (query.Any(employee => employee.Name.IsEquals(employeeDto.Name) && employee.Id != employeeId))
                validations.Errors.Add(new ValidationFailure("Error", "Name must be unique."));
            if (query.Any(employee => employee.Cpf.IsEquals(employeeDto.Cpf.OnlyNumbers()) && employee.Id != employeeId))
                validations.Errors.Add(new ValidationFailure("Error", "CPF must be unique."));
            if (employeeDto.RoleId > 0 && (!employeeDto.CompanyId.HasValue || employeeDto.CompanyId == 0))
                validations.Errors.Add(new ValidationFailure("Error", "CompanyId is required."));

            return validations;
        }
    }
}
