using FluentValidation;
using FluentValidation.Results;
using SIGDB1.Application.Dtos;
using SIGDB1.Domain.Repositories;
using System.Threading.Tasks;
using SIGDB1.Core.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace SIGDB1.Application.Validators
{
    public class RoleValidator : AbstractValidator<RoleDto>, IRoleValidator 
    { 
        private readonly IRoleRepository _roleRepository;
    public RoleValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Description)
            .Must(nome => nome.IsEmpty() == false)
            .WithMessage("Description is required.");

    }

    public async Task<ValidationResult> CreateValidate(CreateRoleDto roleDto)
    {
        var validations = Validate(roleDto);

        if (!validations.IsValid)
            return validations;

        var query = await _roleRepository.Get();
        if (query.Any(role => role.Description.IsEquals(roleDto.Description)))
            validations.Errors.Add(new ValidationFailure("Error", "Description must be unique."));

        return validations;
    }

    public async Task<ValidationResult> UpdateValidate(int roleId, UpdateRoleDto roleDto)
    {
        if (roleId != roleDto.Id)
            return new ValidationResult(new List<ValidationFailure> { new ValidationFailure("Error", "Identification is invalid.") });

        var validations = Validate(roleDto);

        if (!validations.IsValid)
            return validations;

        var query = await _roleRepository.Get();
        if (query.Any(role => role.Description.IsEquals(roleDto.Description) && role.Id != roleId))
            validations.Errors.Add(new ValidationFailure("Error", "Description must be unique."));

        return validations;
    }
}
}
