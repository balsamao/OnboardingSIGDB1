using FluentValidation;
using FluentValidation.Results;
using SIGDB1.Application.Dtos;
using SIGDB1.Core.Extensions;
using SIGDB1.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGDB1.Application.Validators
{
    public class CompanyValidator : AbstractValidator<CompanyDto>, ICompanyValidator
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(x => x.Name)
                .Must(name => name.IsEmpty() == false)
                .WithMessage("Name is required.");

            RuleFor(x => x.Cnpj)
                .Must(cnpj => cnpj.OnlyNumbers().IsEmpty() == false)
                .WithMessage("CNPJ is required.");
        }

        public async Task<ValidationResult> CreateValidate(CreateCompanyDto companyDto)
        {
            var validations = Validate(companyDto);

            if (!validations.IsValid)
                return validations;

            var query = await _companyRepository.Get();
            if (query.Any(company => company.Name.IsEquals(companyDto.Name)))
                validations.Errors.Add(new ValidationFailure("Error", "Name must be unique."));
            if (query.Any(company => company.Cnpj.IsEquals(companyDto.Cnpj.OnlyNumbers())))
                validations.Errors.Add(new ValidationFailure("Error", "CNPJ must be unique."));

            return validations;
        }

        public async Task<ValidationResult> UpdateValidate(int companyId, UpdateCompanyDto companyDto)
        {
            if (companyId != companyDto.Id)
                return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Error", "Identification is invalid.") });

            var validations = Validate(companyDto);

            if (!validations.IsValid)
                return validations;

            var query = await _companyRepository.Get();

            if (query.Any(company => company.Name.IsEquals(companyDto.Name) && company.Id != companyId))
                validations.Errors.Add(new ValidationFailure("Error", "Name must be unique."));
            if (query.Any(company => company.Cnpj.IsEquals(companyDto.Cnpj.OnlyNumbers()) && company.Id != companyId))
                validations.Errors.Add(new ValidationFailure("Error", "CNPJ must be unique."));

            return validations;
        }
    }
}
