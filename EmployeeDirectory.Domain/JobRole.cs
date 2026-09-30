namespace EmployeeDirectory.Domain
{
    public class JobRole
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
    }
}