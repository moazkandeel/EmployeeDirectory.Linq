namespace EmployeeDirectory.Domain
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public DateOnly HireDate { get; set; }

        public int DepartmentId { get; set; }

        public int JobRoleId { get; set; }

        public int BranchId { get; set; }

        public int? ManagerId { get; set; }

        public EmploymentStatus Status { get; set; }
    }
}