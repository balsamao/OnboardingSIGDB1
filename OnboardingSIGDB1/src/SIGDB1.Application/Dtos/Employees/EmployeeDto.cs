using System;

namespace SIGDB1.Application.Dtos
{
    public abstract class EmployeeDto
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Hiring { get; set; }
    }
}
