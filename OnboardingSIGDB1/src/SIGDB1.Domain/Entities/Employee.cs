using SIGDB1.Core.DomainObjects;
using System;

namespace SIGDB1.Domain.Entities
{
    public class Employee : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Cpf { get; private set; }

        public DateTime? Hiring { get; private set; }

        public int? CompanyId { get; private set; }

        public int? RoleId { get; private set; }

        public Company Company { get; set; }

        public Role Role { get; set; }

        protected Employee() 
        {

        }

        public Employee(string name, string cpf)
        {
            Name = name;
            Cpf = cpf;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeCpf(string cpf)
        {
            Cpf = cpf;
        }

        public void AssociateCompany(int? companyId, DateTime? hiring) 
        {
            CompanyId = companyId;
            Hiring = hiring;
        }

        public void StartNewPosition(int? roleId)
        {
            RoleId = roleId;
        }
    }
}
