namespace SIGDB1.Application.Dtos
{
    public class GetEmployeeDto : EmployeeDto
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int? RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
