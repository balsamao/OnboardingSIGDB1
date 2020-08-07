using SIGDB1.Core.DomainObjects;
using System.Collections.Generic;

namespace SIGDB1.Domain.Entities
{
    public class Role : Entity, IAggregateRoot
    {
        public string Description { get; private set; }

        public ICollection<Employee> Employees { get; set; }

        protected Role()
        {
            Employees = new HashSet<Employee>();
        }

        public Role(string description)
        {
            Description = description;

            Employees = new HashSet<Employee>();
        }

        public void ChangeDescription(string description) 
        {
            Description = description;
        }
    }
}
