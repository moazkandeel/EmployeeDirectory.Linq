using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.DatabaseModels;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
