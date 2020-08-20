using AutoMapper;
using SIGDB1.Application.Dtos;
using SIGDB1.Application.Validators;
using SIGDB1.Core.Extensions;
using SIGDB1.Domain.Entities;
using SIGDB1.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGDB1.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleValidator _roleValidator;
        private readonly IMapper _mapper;

        public List<string> Errors { get; }

        public RoleService(IRoleRepository roleRepository, 
                           IRoleValidator roleValidator, 
                           IMapper mapper)
        {
            _roleRepository = roleRepository;
            _roleValidator = roleValidator;
            _mapper = mapper;

            Errors = new List<string>();
        }

        public async Task<List<GetRoleDto>> Filter(FilterRoleDto filter)
        {
            var query = await _roleRepository.Get();

            if (filter.Id > 0)
                query = query.Where(rol => rol.Id == filter.Id);

            if (!filter.Description.IsEmpty())
                query = query.Where(rol => rol.Description.Like(filter.Description));

            if (!query.Any())
                return null;

            return _mapper.Map<List<GetRoleDto>>(query.ToList());
        }

        public async Task<GetRoleDto> Create(CreateRoleDto roleDto)
        {
            var validations = await _roleValidator.CreateValidate(roleDto);
            if (validations.IsValid)
            {
                var id = await _roleRepository.Create(_mapper.Map<Role>(roleDto));

                var result = await Filter(new FilterRoleDto { Id = id });
                return result.FirstOrDefault();
            }

            Errors.AddRange(validations.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<GetRoleDto> Update(int roleId, UpdateRoleDto roleDto)
        {
            var validations = await _roleValidator.UpdateValidate(roleId, roleDto);
            if (validations.IsValid)
            {
                var entity = _mapper.Map<Role>(roleDto);
                entity.ChangeDescription(roleDto.Description);

                await _roleRepository.Update(entity);

                var result = await Filter(new FilterRoleDto { Id = roleId });
                return result.FirstOrDefault();
            }

            Errors.AddRange(validations.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _roleRepository.Delete(id);
        }

        public bool Success() 
        {
            return Errors.IsEmpty();
        }
    }
}
