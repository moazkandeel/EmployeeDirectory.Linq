using EmployeeDirectory.Data;

namespace EmployeeDirectory.Exercises
{
    public static class JoinExercises
    {
        //  Joins employees with their departments using method syntax.
        public record EmployeeDepartmentResult(string EmployeeName, string DepartmentName);

        //  Returns each employee with the name of their department.
        public static IEnumerable<EmployeeDepartmentResult> GetEmployeesWithDepartments()
        {
            return SeedData.Employees
                .Join(
                    SeedData.Departments,
                    e => e.DepartmentId,
                    d => d.Id,
                    (e, d) => new EmployeeDepartmentResult(e.Name, d.Name));
        }


        //  Joins employees with departments using LINQ query syntax.
        public static IEnumerable<EmployeeDepartmentResult> GetEmployeesWithDepartmentsQuerySyntax()
        {
            return
                from e in SeedData.Employees
                join d in SeedData.Departments
                    on e.DepartmentId equals d.Id
                select new EmployeeDepartmentResult(e.Name, d.Name);
        }

        //  Result model for a department and its employees.
        public record DepartmentEmployeesResult( string DepartmentName, IEnumerable<string> EmployeeNames);

        //  Groups employees under each department, including empty departments.
        public static IEnumerable<DepartmentEmployeesResult> GetDepartmentsWithEmployees()
        {
            return SeedData.Departments
                .GroupJoin(
                    SeedData.Employees,
                    d => d.Id,
                    e => e.DepartmentId,
                    (d, employees) => new DepartmentEmployeesResult(
                        d.Name,
                        employees.Select(e => e.Name)));
        }



        //  Result model for an employee and optional department.
        public record EmployeeOptionalDepartmentResult( string EmployeeName, string? DepartmentName);

        //  Returns all employees, including employees without a matching department.
        public static IEnumerable<EmployeeOptionalDepartmentResult> GetEmployeesWithOptionalDepartments()
        {
            return SeedData.OrphanEmployees
                .GroupJoin(
                    SeedData.Departments,
                    e => e.DepartmentId,
                    d => d.Id,
                    (e, departments) => new
                    {
                        Employee = e,
                        Departments = departments
                    })
                .SelectMany(
                    x => x.Departments.DefaultIfEmpty(),
                    (x, d) => new EmployeeOptionalDepartmentResult(
                        x.Employee.Name,
                        d?.Name));
        }



        //  Result model for a full outer join between employees and departments.
        public record FullEmployeeDepartmentResult( string? EmployeeName, string? DepartmentName);

        // B.3.5 - Combines two left joins with Union to simulate a full outer join.
        // B.3.5 - Combines two left joins with Union to simulate a full outer join.
        public static IEnumerable<FullEmployeeDepartmentResult> GetFullEmployeeDepartmentJoin()
        {
            var allEmployees = SeedData.Employees
                .Concat(SeedData.OrphanEmployees);

            var employeesWithDepartments =
                allEmployees
                    .GroupJoin(
                        SeedData.Departments,
                        e => e.DepartmentId,
                        d => d.Id,
                        (e, departments) => new
                        {
                            Employee = e,
                            Departments = departments
                        })
                    .SelectMany(
                        x => x.Departments.DefaultIfEmpty(),
                        (x, d) => new FullEmployeeDepartmentResult(
                            x.Employee.Name,
                            d?.Name));

            var departmentsWithEmployees =
                SeedData.Departments
                    .GroupJoin(
                        allEmployees,
                        d => d.Id,
                        e => e.DepartmentId,
                        (d, employees) => new
                        {
                            Department = d,
                            Employees = employees
                        })
                    .SelectMany(
                        x => x.Employees.DefaultIfEmpty(),
                        (x, e) => new FullEmployeeDepartmentResult(
                            e?.Name,
                            x.Department.Name));

            return employeesWithDepartments
                .Union(departmentsWithEmployees);
        }


        //  Result model for an employee and their manager.
        public record EmployeeManagerResult(  string EmployeeName,  string ManagerName);

        // Uses a self join to match each employee with their manager.
        public static IEnumerable<EmployeeManagerResult> GetEmployeesWithManagers()
        {
            return SeedData.Employees
                .Where(e => e.ManagerId.HasValue)
                .Join(
                    SeedData.Employees,
                    e => e.ManagerId,
                    manager => manager.Id,
                    (e, manager) => new EmployeeManagerResult(
                        e.Name,
                        manager.Name));
        }


        //  Result model for every department and branch combination.
        public record DepartmentBranchResult( string DepartmentName,  string BranchName);


        //  Creates every possible combination of departments and branches.
        public static IEnumerable<DepartmentBranchResult> GetDepartmentBranchCombinations()
        {
            return SeedData.Departments
                .SelectMany(d => SeedData.Branches, (d, b) => new DepartmentBranchResult( d.Name, b.Name));
        }

        //  Result model for an employee and one of their skills.
        public record EmployeeSkillResult(  string EmployeeName,  string SkillName);


        // B.3.8 - Joins employees with their skills through the EmployeeSkill linking collection.
        public static IEnumerable<EmployeeSkillResult> GetEmployeesWithSkills()
        {
            return SeedData.Employees
                .Join(
                    SeedData.EmployeeSkills,
                    e => e.Id,
                    es => es.EmployeeId,
                    (e, es) => new
                    {
                        Employee = e,
                        EmployeeSkill = es
                    })
                .Join(
                    SeedData.Skills,
                    x => x.EmployeeSkill.SkillId,
                    s => s.Id,
                    (x, s) => new EmployeeSkillResult(
                        x.Employee.Name,
                        s.Name));
        }


    }
}