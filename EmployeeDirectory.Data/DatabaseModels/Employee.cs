using System;
using System.Collections.Generic;

namespace EmployeeDirectory.Data.DatabaseModels;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public DateOnly HireDate { get; set; }

    public int DepartmentId { get; set; }

    public int JobRoleId { get; set; }

    public int BranchId { get; set; }

    public int? ManagerId { get; set; }

    public int Status { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual JobRole JobRole { get; set; } = null!;

    public virtual Employee? Manager { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
