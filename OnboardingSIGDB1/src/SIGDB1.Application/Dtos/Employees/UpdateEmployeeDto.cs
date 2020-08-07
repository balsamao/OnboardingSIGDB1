namespace SIGDB1.Application.Dtos
{
    public class UpdateEmployeeDto : EmployeeDto
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public int? RoleId { get; set; }
    }
}
