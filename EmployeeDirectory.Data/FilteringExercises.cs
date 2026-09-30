using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;
using EmployeeDirectory.Exercises;

namespace EmployeeDirectory.Exercises
{
    public static class FilteringExercises
    {
        public record EmployeeSalaryResult(string Name, decimal Salary);
        public record EmployeeIndexResult(int Index, string Name);
        public record EmployeeSummaryResult(string EmployeeName, string DepartmentName);

        public static IEnumerable<EmployeeSalaryResult> GetHighSalaryEmployees()
        {
            return SeedData.Employees
                .Where(e => e.Salary > 12000)
                .Select(e => new EmployeeSalaryResult(e.Name, e.Salary));
        }

        public static IEnumerable<EmployeeIndexResult> GetEmployeesWithIndex()

        {
            return SeedData.Employees
                .Select((e, index) => new EmployeeIndexResult(index, e.Name));
        }

        public static IEnumerable<EmployeeSummaryResult> GetEmployeeSummaries()
        {
            return SeedData.Employees
                .Join(
                    SeedData.Departments,
                    e => e.DepartmentId,
                    d => d.Id,
                    (e, d) => new EmployeeSummaryResult(e.Name, d.Name));
        }

        public static IEnumerable<Employee> GetEmployeesWithDistinctSalaries()
        {
            return SeedData.Employees
                .DistinctBy(e => e.Salary);
        }
    }
}