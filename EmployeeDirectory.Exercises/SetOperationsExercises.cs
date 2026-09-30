using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Exercises
{
    public static class SetOperationsExercises
    {
        // B.4.1 - Result model for an employee name.
        public record EmployeeNameResult(string Name);

        // B.4.1 - Combines employees from two groups and removes duplicates.
        public static IEnumerable<EmployeeNameResult> GetUnionOfEmployeeGroups()
        {
            var firstGroup = SeedData.Employees
                .Where(e => e.Salary >= 12000)
                .Select(e => new EmployeeNameResult(e.Name));

            var secondGroup = SeedData.Employees
                .Where(e => e.Status == EmploymentStatus.Active)
                .Select(e => new EmployeeNameResult(e.Name));

            return firstGroup.Union(secondGroup);
        }

        // B.4.2 - Result model for comparing Concat and Union.
        public record ConcatUnionResult(
            int ConcatCount,
            int UnionCount);

        // B.4.2 - Compares Concat, which keeps duplicates, with Union, which removes them.
        public static ConcatUnionResult CompareConcatAndUnion()
        {
            var firstGroup = SeedData.Employees
                .Where(e => e.Salary >= 12000);

            var secondGroup = SeedData.Employees
                .Where(e => e.Status == EmploymentStatus.Active);

            var concatCount = firstGroup
                .Concat(secondGroup)
                .Count();

            var unionCount = firstGroup
                .Union(secondGroup)
                .Count();

            return new ConcatUnionResult(
                concatCount,
                unionCount);
        }


        //  Result model for a shared employee name.
        public record SharedEmployeeResult(string Name);


        //  Returns employees who belong to both groups.
        public static IEnumerable<SharedEmployeeResult> GetIntersectingEmployees()
        {
            var firstGroup = SeedData.Employees
                .Where(e => e.Salary >= 12000)
                .Select(e => e.Name);

            var secondGroup = SeedData.Employees
                .Where(e => e.Status == EmploymentStatus.Active)
                .Select(e => e.Name);

            return firstGroup
                .Intersect(secondGroup)
                .Select(name => new SharedEmployeeResult(name));
        }

        //  Result model for employees remaining after Except.
        public record ExceptEmployeeResult(string Name);

        //  Returns employees in the first group that are not in the second group.
        public static IEnumerable<ExceptEmployeeResult> GetEmployeesExceptActive()
        {
            var highSalaryEmployees = SeedData.Employees
                .Where(e => e.Salary >= 12000)
                .Select(e => e.Name);

            var activeEmployees = SeedData.Employees
                .Where(e => e.Status == EmploymentStatus.Active)
                .Select(e => e.Name);

            return highSalaryEmployees
                .Except(activeEmployees)
                .Select(name => new ExceptEmployeeResult(name));
        }

        //  Result model for comparing employees by department.
        public record EmployeeDepartmentSetResult( int DepartmentId, string Name);

        //  Demonstrates IntersectBy, ExceptBy, and UnionBy using DepartmentId as the key.
        public static (
            IEnumerable<EmployeeDepartmentSetResult> IntersectByResult,
            IEnumerable<EmployeeDepartmentSetResult> ExceptByResult,
            IEnumerable<EmployeeDepartmentSetResult> UnionByResult)
            GetDepartmentSetOperationsByKey()
        {
            var firstGroup = SeedData.Employees
                .Where(e => e.Id <= 10)
                .Select(e => new EmployeeDepartmentSetResult(e.DepartmentId, e.Name));

            var secondGroup = SeedData.Employees
                .Where(e => e.Id > 10 && e.Id <= 20)
                .Select(e => new EmployeeDepartmentSetResult(e.DepartmentId, e.Name));

            var secondDepartmentIds = secondGroup
                .Select(e => e.DepartmentId);

            var intersectByResult = firstGroup
                .IntersectBy(secondDepartmentIds, e => e.DepartmentId);

            var exceptByResult = firstGroup
                .ExceptBy(secondDepartmentIds, e => e.DepartmentId);

            var unionByResult = firstGroup
                .UnionBy(secondGroup, e => e.DepartmentId);

            return (
                intersectByResult,
                exceptByResult,
                unionByResult);
        }
        // Compares employee names without considering letter casing.
        public class EmployeeNameComparer : IEqualityComparer<string>
        {
            public bool Equals(string? x, string? y)
            {
                return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(string obj)
            {
                return StringComparer.OrdinalIgnoreCase.GetHashCode(obj);
            }
        }

        //  Uses a custom comparer to treat employee names with different casing as equal.
        public static IEnumerable<string> GetDistinctNamesIgnoringCase()
        {
            var names = new[]
            {
        "Ahmed Hassan",
        "ahmed hassan",
        "Mona Ali",
        "mona ali",
        "Omar Mohamed"
    };

            return names.Distinct(new EmployeeNameComparer());
        }


    }

}