using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.DatabaseModels;

public partial class JobRole
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
