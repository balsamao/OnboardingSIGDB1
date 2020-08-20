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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyValidator _companyValidator;
        private readonly IMapper _mapper;

        public List<string> Errors { get; }

        public CompanyService(ICompanyRepository companyRepository, 
                              ICompanyValidator companyValidator, 
                              IMapper mapper)
        {
            _companyRepository = companyRepository;
            _companyValidator = companyValidator;
            _mapper = mapper;

            Errors = new List<string>();
        }

        public async Task<List<GetCompanyDto>> Filter(FilterCompanyDto filter)
        {
            var query = await _companyRepository.Get();

            if (filter.Id > 0)
                query = query.Where(comp => comp.Id == filter.Id);

            if (!filter.Name.IsEmpty())
                query = query.Where(comp => comp.Name.IsEquals(filter.Name));

            if (!filter.Cnpj.IsEmpty())
                query = query.Where(comp => comp.Cnpj.IsEquals(filter.Cnpj.OnlyNumbers()));

            if (!filter.Fundation.IsEmpty())
                query = query.Where(comp => comp.Fundation.HasValue && comp.Fundation.Value.Date == filter.Fundation.ToDate());

            if (!query.Any())
                return null;

            return _mapper.Map<List<GetCompanyDto>>(query.ToList());
        }

        public async Task<GetCompanyDto> Create(CreateCompanyDto companyDto)
        {
            var validations = await _companyValidator.CreateValidate(companyDto);
            if (validations.IsValid)
            {
                await _companyRepository.Create(_mapper.Map<Company>(companyDto));

                var result = await Filter(new FilterCompanyDto { Name = companyDto.Name, Cnpj = companyDto.Cnpj, Fundation = companyDto.Fundation });
            }
            Errors.AddRange(validations.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<GetCompanyDto> Update(int companyId, UpdateCompanyDto companyDto)
        {
            var validations = await _companyValidator.UpdateValidate(companyId, companyDto);
            if (validations.IsValid)
            {
                var entity = _mapper.Map<Company>(companyDto);
                entity.ChangeName(companyDto.Name);
                entity.ChangeCnpj(companyDto.Cnpj);
                entity.AlterDateFundation(companyDto.Fundation.ToDate());

                await _companyRepository.Update(entity);

                var result = await Filter(new FilterCompanyDto { Name = companyDto.Name, Cnpj = companyDto.Cnpj, Fundation = companyDto.Fundation });
            }
            Errors.AddRange(validations.Errors.Select(erro => erro.ErrorMessage));

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            return await _companyRepository.Delete(id);
        }

        public bool Success()
        {
            return Errors.IsEmpty();
        }
    }
}
