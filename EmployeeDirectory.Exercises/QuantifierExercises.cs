using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Exercises
{
    public static class QuantifierExercises
    {
        //  Result model for demonstrating Any and All.
        public record EmployeeQuantifierResult(
            bool HasHighSalaryEmployee,
            bool AllEmployeesHavePositiveSalary);

        // Uses Any to check whether at least one employee earns more than 15000.
        // Uses All to check whether every employee has a positive salary.
        public static EmployeeQuantifierResult CheckEmployeeConditions()
        {
            var hasHighSalaryEmployee = SeedData.Employees
                .Any(e => e.Salary > 15000);

            var allEmployeesHavePositiveSalary = SeedData.Employees
                .All(e => e.Salary > 0);

            return new EmployeeQuantifierResult(
                hasHighSalaryEmployee,
                allEmployeesHavePositiveSalary);
        }



        //  Result model for employees who have at least one required skill.
        public record EmployeeSkillMatchResult(
            string EmployeeName,
            bool HasRequiredSkill);

        //  Uses a HashSet of skill IDs with Contains to check whether
        // each employee has at least one skill from the required skill set.
        public static IEnumerable<EmployeeSkillMatchResult> GetEmployeesWithRequiredSkills()
        {
            var requiredSkillIds = new HashSet<int>
    {
        1, // C#
        2, // SQL
        3  // LINQ
    };

            return SeedData.Employees
                .Select(employee => new EmployeeSkillMatchResult(
                    employee.Name,
                    SeedData.EmployeeSkills
                        .Where(es => es.EmployeeId == employee.Id)
                        .Select(es => es.SkillId)
                        .Any(skillId => requiredSkillIds.Contains(skillId))));
        }


        //   Result model for employees earning above their own department average.
        public record EmployeeAboveDepartmentAverageResult(
            string EmployeeName,
            int DepartmentId,
            decimal Salary,
            decimal DepartmentAverage);

        //  Naive approach: calculates the department average separately
        // for each employee.
        public static IEnumerable<EmployeeAboveDepartmentAverageResult> GetEmployeesAboveDepartmentAverageNaive()
        {
            return SeedData.Employees
                .Select(employee =>
                {
                    var departmentAverage = SeedData.Employees
                        .Where(e => e.DepartmentId == employee.DepartmentId)
                        .Average(e => e.Salary);

                    return new
                    {
                        employee.Name,
                        employee.DepartmentId,
                        employee.Salary,
                        DepartmentAverage = departmentAverage
                    };
                })
                .Where(result => result.Salary > result.DepartmentAverage)
                .Select(result => new EmployeeAboveDepartmentAverageResult(
                    result.Name,
                    result.DepartmentId,
                    result.Salary,
                    result.DepartmentAverage));
        }

        // - Optimized approach: calculates each department average once
        // and stores the result in a dictionary for fast lookup.
        public static IEnumerable<EmployeeAboveDepartmentAverageResult> GetEmployeesAboveDepartmentAverageDictionary()
        {
            var departmentAverages = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .ToDictionary(
                    group => group.Key,
                    group => group.Average(e => e.Salary));

            return SeedData.Employees
                .Where(employee =>
                    employee.Salary > departmentAverages[employee.DepartmentId])
                .Select(employee => new EmployeeAboveDepartmentAverageResult(
                    employee.Name,
                    employee.DepartmentId,
                    employee.Salary,
                    departmentAverages[employee.DepartmentId]));
        }

        //  Result model for employees who do not have any of the top three skills.
        public record EmployeeWithoutTopSkillsResult(
            string EmployeeName);

        //  Finds the three most common skills and returns employees
        // who do not have any of those skills.
        public static IEnumerable<EmployeeWithoutTopSkillsResult> GetEmployeesWithoutTopThreeSkills()
        {
            var topThreeSkillIds = SeedData.EmployeeSkills
                .GroupBy(es => es.SkillId)
                .OrderByDescending(group => group.Count())
                .ThenBy(group => group.Key)
                .Take(3)
                .Select(group => group.Key)
                .ToHashSet();

            return SeedData.Employees
                .Where(employee =>
                    !SeedData.EmployeeSkills
                        .Where(es => es.EmployeeId == employee.Id)
                        .Any(es => topThreeSkillIds.Contains(es.SkillId)))
                .Select(employee => new EmployeeWithoutTopSkillsResult(
                    employee.Name));
        }

        //  Result model for employees earning above the company-wide average salary.
        public record EmployeeAboveCompanyAverageResult(
            string EmployeeName,
            decimal Salary,
            decimal CompanyAverageSalary);

        //  Calculates the company-wide average salary once and returns
        // employees whose salary is higher than that scalar value.
        public static IEnumerable<EmployeeAboveCompanyAverageResult> GetEmployeesAboveCompanyAverage()
        {
            var companyAverageSalary = SeedData.Employees
                .Average(e => e.Salary);

            return SeedData.Employees
                .Where(employee => employee.Salary > companyAverageSalary)
                .Select(employee => new EmployeeAboveCompanyAverageResult(
                    employee.Name,
                    employee.Salary,
                    companyAverageSalary));
        }


        //  Result model for an employee found in a manager hierarchy.
        public record EmployeeDescendantResult(
            int EmployeeId,
            string EmployeeName,
            int? ManagerId);

        // Recursively returns all direct and indirect employees under a manager.
        // yield return keeps the traversal lazy and avoids building the complete result at once.
        public static IEnumerable<EmployeeDescendantResult> GetDescendants(int managerId)
        {
            foreach (var employee in SeedData.Employees
                .Where(e => e.ManagerId == managerId))
            {
                yield return new EmployeeDescendantResult(
                    employee.Id,
                    employee.Name,
                    employee.ManagerId);

                foreach (var descendant in GetDescendants(employee.Id))
                {
                    yield return descendant;
                }
            }
        }



        //  Result model for an employee hierarchy starting from a manager.
        public record ManagerHierarchyResult(
            int EmployeeId,
            string EmployeeName,
            int? ManagerId);

        // Returns the manager together with every employee directly or indirectly
        // reporting to that manager.
        public static IEnumerable<ManagerHierarchyResult> GetEveryoneUnderManager(int managerId)
        {
            var manager = SeedData.Employees
                .SingleOrDefault(e => e.Id == managerId);

            if (manager is null)
                return Enumerable.Empty<ManagerHierarchyResult>();

            return new[]
                {
            new ManagerHierarchyResult(
                manager.Id,
                manager.Name,
                manager.ManagerId)
        }
                .Concat(
                    GetDescendants(managerId)
                        .Select(employee => new ManagerHierarchyResult(
                            employee.EmployeeId,
                            employee.EmployeeName,
                            employee.ManagerId)));
        }



        //  Result model for an employee and their management path.
        public record EmployeeManagementPathResult(
            string EmployeeName,
            string ManagementPath);

        //  Builds the management path from the top-level manager down to the employee.
        public static IEnumerable<EmployeeManagementPathResult> GetEmployeeManagementPaths()
        {
            return SeedData.Employees
                .Select(employee => new EmployeeManagementPathResult(
                    employee.Name,
                    BuildManagementPath(employee.Id)));
        }

        //  Recursively builds a management path for one employee.
        private static string BuildManagementPath(int employeeId)
        {
            var employee = SeedData.Employees
                .Single(e => e.Id == employeeId);

            if (!employee.ManagerId.HasValue)
                return employee.Name;

            return $"{BuildManagementPath(employee.ManagerId.Value)} → {employee.Name}";
        }




        // B.8.4 - Result model for an employee and their management path.
        public record SafeEmployeeManagementPathResult(
            string EmployeeName,
            string ManagementPath);

        //  Builds management paths while detecting cycles in the manager hierarchy.
        public static IEnumerable<SafeEmployeeManagementPathResult> GetSafeEmployeeManagementPaths(
            IEnumerable<Employee> employees)
        {
            return employees.Select(employee => new SafeEmployeeManagementPathResult(
                employee.Name,
                BuildSafeManagementPath(employee.Id, employees, new HashSet<int>())));
        }

        //  Recursively builds a management path and throws when a cycle is detected.
        private static string BuildSafeManagementPath(
            int employeeId,
            IEnumerable<Employee> employees,
            HashSet<int> visited)
        {
            if (!visited.Add(employeeId))
                throw new InvalidOperationException(
                    $"Cycle detected in employee hierarchy at employee ID {employeeId}.");

            var employee = employees
                .Single(e => e.Id == employeeId);

            if (!employee.ManagerId.HasValue)
                return employee.Name;

            return $"{BuildSafeManagementPath(
                employee.ManagerId.Value,
                employees,
                visited)} → {employee.Name}";
        }



        // B.9.1 - Result model for demonstrating deferred and immediate execution.
        public record ExecutionTimingResult(
            int DeferredResultCount,
            int ImmediateResultCount,
            int OriginalEmployeeCount,
            int DeferredResultCountAfterSourceChange);

        // Demonstrates deferred execution versus immediate execution.
        // The deferred query observes the source when it is enumerated,
        // while ToList() executes the immediate query at creation time.
        public static ExecutionTimingResult DemonstrateDeferredAndImmediateExecution()
        {
            var originalEmployees = SeedData.Employees;

            var changedEmployees = originalEmployees
                .Append(
                    new Employee
                    {
                        Id = 999,
                        Name = "Deferred Test Employee",
                        Salary = 20000,
                        HireDate = new DateOnly(2026, 1, 1),
                        DepartmentId = 1,
                        JobRoleId = 1,
                        BranchId = 1,
                        ManagerId = null,
                        Status = EmploymentStatus.Active
                    });

            var deferredQuery = changedEmployees
                .Where(e => e.Salary > 12000);

            var immediateQuery = originalEmployees
                .Where(e => e.Salary > 12000)
                .ToList();

            var originalEmployeeCount = originalEmployees.Count;

            return new ExecutionTimingResult(
                deferredQuery.Count(),
                immediateQuery.Count,
                originalEmployeeCount,
                deferredQuery.Count());
        }



        //  Result model for comparing repeated enumeration with materialization.
        public record MultipleEnumerationResult(
            int FirstEnumerationCount,
            int SecondEnumerationCount,
            int MaterializedCount,
            int MaterializedCountAgain);

        //  Demonstrates that enumerating an IEnumerable query multiple times
        // executes the query each time, while a materialized List can be reused.
        public static MultipleEnumerationResult DemonstrateMultipleEnumeration()
        {
            var employees = SeedData.Employees;

            var query = employees
                .Where(e => e.Salary > 12000);

            var firstEnumerationCount = query.Count();
            var secondEnumerationCount = query.Count();

            var materializedResult = query.ToList();

            var materializedCount = materializedResult.Count;
            var materializedCountAgain = materializedResult.Count;

            return new MultipleEnumerationResult(
                firstEnumerationCount,
                secondEnumerationCount,
                materializedCount,
                materializedCountAgain);
        }



        // - Result model for measuring LINQ performance on 100,000 employees.
        public record PerformanceTimingResult(
            int EmployeeCount,
            int MatchingEmployeeCount,
            long QueryMilliseconds,
            long MaterializationMilliseconds);

        //  Generates 100,000 employees and measures the time required
        // to execute and materialize a LINQ filtering query.
        public static PerformanceTimingResult MeasurePerformance()
        {
            var employees = Enumerable
                .Range(1, 100_000)
                .Select(id => new Employee
                {
                    Id = id,
                    Name = $"Employee {id}",
                    Salary = 8000 + (id % 10000),
                    HireDate = new DateOnly(2020, 1, 1).AddDays(id % 2000),
                    DepartmentId = (id % 5) + 1,
                    JobRoleId = (id % 6) + 1,
                    BranchId = (id % 3) + 1,
                    ManagerId = null,
                    Status = EmploymentStatus.Active
                })
                .ToList();

            var query = employees
                .Where(e => e.Salary > 12000);

            var queryStopwatch = System.Diagnostics.Stopwatch.StartNew();

            var matchingEmployeeCount = query.Count();

            queryStopwatch.Stop();

            var materializationStopwatch =
                System.Diagnostics.Stopwatch.StartNew();

            var materializedEmployees = query.ToList();

            materializationStopwatch.Stop();

            return new PerformanceTimingResult(
                employees.Count,
                matchingEmployeeCount,
                queryStopwatch.ElapsedMilliseconds,
                materializationStopwatch.ElapsedMilliseconds);
        }






    }
}