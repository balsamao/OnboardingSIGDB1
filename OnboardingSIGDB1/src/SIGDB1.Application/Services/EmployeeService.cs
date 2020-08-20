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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IMapper _mapper;

        public List<string> Errors { get; }

        public EmployeeService(IEmployeeRepository employeeRepository, 
                               IEmployeeValidator employeeValidator, 
                               IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _mapper = mapper;

            Errors = new List<string>();
        }

        public async Task<List<GetEmployeeDto>> Filter(FilterEmployeeDto filter)
        {
            var query = await _employeeRepository.Get();

            if (filter.Id > 0)
                query = query.Where(emp => emp.Id == filter.Id);

            if (!filter.Name.IsEmpty())
                query = query.Where(emp => emp.Name.Like(filter.Name));

            if (!filter.Cpf.IsEmpty())
                query = query.Where(emp => emp.Cpf.Like(filter.Cpf.OnlyNumbers()));

            if (!filter.Hiring.IsEmpty())
                query = query.Where(emp => emp.Hiring.HasValue && emp.Hiring.Value.Date == filter.Hiring.ToDate());

            if (!query.Any())
                return null;

            return _mapper.Map<List<GetEmployeeDto>>(query.ToList());
        }

        public async Task<GetEmployeeDto> Create(CreateEmployeeDto employeeDto)
        {
            var validations = await _employeeValidator.CreateValidate(employeeDto);
            if (validations.IsValid)
            {
                var id = await _employeeRepository.Create(_mapper.Map<Employee>(employeeDto));

                var result = await Filter(new FilterEmployeeDto { Id = id });
                return result.FirstOrDefault();
            }
            Errors.AddRange(validations.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<GetEmployeeDto> Update(int employeeId, UpdateEmployeeDto employeeDto)
        {
            var validacoes = await _employeeValidator.UpdateValidate(employeeId, employeeDto);
            if (validacoes.IsValid)
            {
                var entity = _mapper.Map<Employee>(employeeDto);

                if (employeeDto.CompanyId != 0)
                    entity.AssociateCompany(employeeDto.CompanyId, employeeDto.Hiring.ToDate());

                if (employeeDto.RoleId != 0)
                    entity.StartNewPosition(employeeDto.RoleId);

                await _employeeRepository.Update(entity);

                var result = await Filter(new FilterEmployeeDto { Id = employeeId });
                return result.FirstOrDefault();
            }

            Errors.AddRange(validacoes.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _employeeRepository.Delete(id);
        }

        public bool Success() 
        {
            return Errors.IsEmpty();
        }
    }
}
