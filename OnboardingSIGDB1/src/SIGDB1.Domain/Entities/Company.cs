using SIGDB1.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace SIGDB1.Domain.Entities
{
    public class Company : Entity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Cnpj { get; private set; }

        public DateTime? Fundation { get; private set; }

        public ICollection<Employee> Employees { get; set; }

        protected Company()
        {
            Employees = new HashSet<Employee>();
        }

        public Company(string name, string cnpj, DateTime? dateFundation) 
        {
            Name = name;
            Cnpj = cnpj;
            Fundation = dateFundation;

            Employees = new HashSet<Employee>();
        }

        public void ChangeName(string name) 
        {
            Name = name;
        }

        public void ChangeCnpj(string cnpj)
        {
            Cnpj = cnpj;
        }

        public void AlterDateFundation(DateTime? dateFundation)
        {
            Fundation = dateFundation;
        }
    }
}
