using System;

namespace SIGDB1.Application.Dtos
{
    public abstract class CompanyDto
    {
        public string Name { get; set; }

        public string Cnpj { get; set; }

        public string Fundation { get; set; }
    }
}
