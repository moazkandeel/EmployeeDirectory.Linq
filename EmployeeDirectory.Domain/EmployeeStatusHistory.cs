namespace EmployeeDirectory.Domain
{
    public class EmployeeStatusHistory
    {
        public int EmployeeId { get; set; }

        public EmploymentStatus Status { get; set; }

        public DateOnly ChangedAt { get; set; }
    }
}