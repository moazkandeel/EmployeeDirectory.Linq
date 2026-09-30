using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.DatabaseModels;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentDepartmentId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

    public virtual ICollection<JobRole> JobRoles { get; set; } = new List<JobRole>();

    public virtual Department? ParentDepartment { get; set; }
}
