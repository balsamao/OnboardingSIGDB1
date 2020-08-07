using AutoMapper;
using SIGDB1.Application.Dtos;
using SIGDB1.Core.Extensions;
using SIGDB1.Domain.Entities;

namespace SIGDB1.Application.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<CreateRoleDto, Role>()
                .ConstructUsing(p => new Role(p.Description));

            CreateMap<UpdateRoleDto, Role>()
                .ConstructUsing(p => new Role(p.Description));

            CreateMap<Role, GetRoleDto>();

            CreateMap<CreateCompanyDto, Company>()
                .ConstructUsing(p => new Company(p.Name, p.Cnpj.OnlyNumbers(), p.Fundation))
                .ForMember(entity => entity.Cnpj, opt => opt.MapFrom(dto => dto.Cnpj.OnlyNumbers()));

            CreateMap<UpdateCompanyDto, Company>()
                .ConstructUsing(p => new Company(p.Name, p.Cnpj.OnlyNumbers(), p.Fundation))
                .ForMember(entity => entity.Cnpj, opt => opt.MapFrom(dto => dto.Cnpj.OnlyNumbers()));

            CreateMap<Company, GetCompanyDto>();

            CreateMap<CreateEmployeeDto, Employee>()
                .ConstructUsing(p => new Employee(p.Name, p.Cpf))
                .ForMember(entity => entity.Cpf, opt => opt.MapFrom(dto => dto.Cpf.OnlyNumbers()));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ConstructUsing(p => new Employee(p.Name, p.Cpf))
                .ForMember(entity => entity.Cpf, opt => opt.MapFrom(dto => dto.Cpf.OnlyNumbers()));

            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dto => dto.CompanyName, opt => opt.MapFrom(entity => entity.Company != null ? entity.Company.Name : ""))
                .ForMember(dto => dto.RoleName, opt => opt.MapFrom(entity => entity.Role != null ? entity.Role.Description : ""));
        }
    }
}
