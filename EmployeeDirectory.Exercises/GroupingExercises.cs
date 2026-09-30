using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Exercises
{
    public static class GroupingExercises
    {
        //  Result model for department salary aggregates.
        public record DepartmentAggregateResult(
            string DepartmentName,
            int EmployeeCount,
            decimal AverageSalary,
            decimal MaxSalary,
            decimal MinSalary);

        //  Groups employees by department and calculates salary aggregates.
        public static IEnumerable<DepartmentAggregateResult> GetDepartmentSalaryAggregates()
        {
            return SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new DepartmentAggregateResult(
                        department.Name,
                        group.Count(),
                        group.Average(e => e.Salary),
                        group.Max(e => e.Salary),
                        group.Min(e => e.Salary)));
        }

        //  Result model for departments that meet an aggregate condition.
        public record DepartmentEmployeeCountResult(
            string DepartmentName,
            int EmployeeCount);

        //  Groups employees by department and keeps only departments with at least 3 employees.
        public static IEnumerable<DepartmentEmployeeCountResult> GetDepartmentsWithAtLeastThreeEmployees()
        {
            return SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Where(group => group.Count() >= 3)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new DepartmentEmployeeCountResult(
                        department.Name,
                        group.Count()));
        }


        //  Result model for grouping employees by department and employment status.
        public record DepartmentStatusGroupResult(
            int DepartmentId,
            EmploymentStatus Status,
            int EmployeeCount);

        //  Groups employees using both DepartmentId and EmploymentStatus as a composite key.
        public static IEnumerable<DepartmentStatusGroupResult> GetEmployeesGroupedByDepartmentAndStatus()
        {
            return SeedData.Employees
                .GroupBy(e => new
                {
                    e.DepartmentId,
                    e.Status
                })
                .Select(group => new DepartmentStatusGroupResult(
                    group.Key.DepartmentId,
                    group.Key.Status,
                    group.Count()));
        }

        // Result model for employee salary statistics and running total.
        public record EmployeeSalaryStatisticsResult(
            int EmployeeCount,
            decimal TotalSalary,
            decimal AverageSalary,
            decimal MaxSalary,
            decimal MinSalary);

        // Calculates employee salary statistics in a single Aggregate pass.
        public static EmployeeSalaryStatisticsResult GetSalaryStatistics()
        {
            var result = SeedData.Employees
                .Aggregate(
                    new EmployeeSalaryStatisticsResult(
                        0,
                        0,
                        0,
                        decimal.MinValue,
                        decimal.MaxValue),
                    (result, employee) =>
                        new EmployeeSalaryStatisticsResult(
                            result.EmployeeCount + 1,
                            result.TotalSalary + employee.Salary,
                            0,
                            Math.Max(result.MaxSalary, employee.Salary),
                            Math.Min(result.MinSalary, employee.Salary)));

            return result with
            {
                AverageSalary = result.EmployeeCount > 0
                    ? result.TotalSalary / result.EmployeeCount
                    : 0
            };
        }


        //  Result model for each employee and the running salary total up to that employee.
        public record EmployeeRunningTotalResult(
            string EmployeeName,
            decimal Salary,
            decimal RunningTotal);

        //  Calculates a running salary total using Aggregate without mutating
        //  an existing collection during the aggregation.
        public static IEnumerable<EmployeeRunningTotalResult> GetRunningSalaryTotals()
        {
            var result = SeedData.Employees
                .Aggregate(
                    (
                        RunningTotal: 0m,
                        Results: Enumerable.Empty<EmployeeRunningTotalResult>()
                    ),
                    (accumulator, employee) =>
                    {
                        var newRunningTotal =
                            accumulator.RunningTotal + employee.Salary;

                        var newResult =
                            new EmployeeRunningTotalResult(
                                employee.Name,
                                employee.Salary,
                                newRunningTotal);

                        return (
                            RunningTotal: newRunningTotal,
                            Results: accumulator.Results.Append(newResult)
                        );
                    });

            return result.Results;
        }


        //  Result model for comparing ToLookup, GroupBy, and ToDictionary.
        public record EmployeeGroupingComparisonResult(
            int LookupCount,
            int GroupByCount,
            int DictionaryCount);

        // Compares the number of department groups produced by ToLookup, GroupBy, and ToDictionary.
        public static EmployeeGroupingComparisonResult CompareGroupingMethods()
        {
            var lookup = SeedData.Employees
                .ToLookup(e => e.DepartmentId);

            var groupBy = SeedData.Employees
                .GroupBy(e => e.DepartmentId);

            var dictionary = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList());

            return new EmployeeGroupingComparisonResult(
                lookup.Count,
                groupBy.Count(),
                dictionary.Count);
        }


        //  Result model for ROLLUP-like department and status totals.
        public record DepartmentStatusRollupResult(
            string? DepartmentName,
            EmploymentStatus? Status,
            int EmployeeCount);

        //  Creates detail rows, department subtotals, and a grand total using LINQ.
        public static IEnumerable<DepartmentStatusRollupResult> GetDepartmentStatusRollup()
        {
            var details = SeedData.Employees
                .Join(
                    SeedData.Departments,  e => e.DepartmentId,  d => d.Id,
                    (e, d) => new
                    {
                        DepartmentName = d.Name,  e.Status
                    })
                .GroupBy(x => new
                {
                    x.DepartmentName, x.Status
                })
                .Select(group => new DepartmentStatusRollupResult(
                    group.Key.DepartmentName, group.Key.Status,
                    group.Count()));

            var departmentTotals = SeedData.Employees
                .Join(
                    SeedData.Departments,  e => e.DepartmentId,  d => d.Id,
                    (e, d) => new
                    {
                        DepartmentName = d.Name
                    })
                .GroupBy(x => x.DepartmentName)
                .Select(group => new DepartmentStatusRollupResult( group.Key,  null,
                    group.Count()));

            var grandTotal = new[] {
        new DepartmentStatusRollupResult(  null,  null,  SeedData.Employees.Count)
         };

            return details
                .Concat(departmentTotals)
                .Concat(grandTotal);
        }


        //  Result model for assigning a row number to each employee.
        public record EmployeeRowNumberResult(
            int RowNumber,
            string EmployeeName,
            decimal Salary);

        //  Orders employees by salary and assigns a sequential row number starting from 1.
        public static IEnumerable<EmployeeRowNumberResult> GetEmployeesWithRowNumber()
        {
            return SeedData.Employees
                .OrderBy(e => e.Salary)
                .ThenBy(e => e.Name)
                .Select((employee, index) => new EmployeeRowNumberResult(
                    index + 1,
                    employee.Name,
                    employee.Salary));
        }



        //  Result model for assigning ranks based on employee salary.
        public record EmployeeRankResult(
            int Rank,
            string EmployeeName,
            decimal Salary);

        //  Assigns the same rank to employees with the same salary and keeps gaps after ties.
        public static IEnumerable<EmployeeRankResult> GetEmployeesWithRank()
        {
            var orderedEmployees = SeedData.Employees
                .OrderByDescending(e => e.Salary)
                .ThenBy(e => e.Name)
                .ToList();

            return orderedEmployees
                .Select((employee, index) => new EmployeeRankResult(
                    orderedEmployees
                        .Take(index)
                        .Count(e => e.Salary > employee.Salary) + 1,
                    employee.Name,
                    employee.Salary));
        }




        //  Result model for assigning dense ranks based on employee salary.
        public record EmployeeDenseRankResult(
            int DenseRank,
            string EmployeeName,
            decimal Salary);

        // Assigns the same dense rank to equal salaries without gaps between ranks.
        public static IEnumerable<EmployeeDenseRankResult> GetEmployeesWithDenseRank()
        {
            var orderedSalaries = SeedData.Employees
                .Select(e => e.Salary)
                .Distinct()
                .OrderByDescending(salary => salary)
                .ToList();

            return SeedData.Employees
                .OrderByDescending(e => e.Salary)
                .ThenBy(e => e.Name)
                .Select(employee => new EmployeeDenseRankResult(
                    orderedSalaries.IndexOf(employee.Salary) + 1,
                    employee.Name,
                    employee.Salary));
        }



        //  Result model for assigning employees to salary quartiles.
        public record EmployeeQuartileResult(
            string EmployeeName,
            decimal Salary,
            int Quartile);

        //  Divides employees into four salary groups similar to SQL NTILE(4).
        public static IEnumerable<EmployeeQuartileResult> GetEmployeesBySalaryQuartile()
        {
            var orderedEmployees = SeedData.Employees
                .OrderByDescending(e => e.Salary)
                .ThenBy(e => e.Name)
                .ToList();

            var employeeCount = orderedEmployees.Count;

            return orderedEmployees
                .Select((employee, index) => new EmployeeQuartileResult(
                    employee.Name,
                    employee.Salary,
                    (index * 4 / employeeCount) + 1));
        }



        //  Result model for accessing the previous and next employee salary.
        public record EmployeeLagLeadResult(
            string EmployeeName,
            decimal Salary,
            decimal? PreviousSalary,
            decimal? NextSalary);

        // Simulates SQL LAG and LEAD by pairing each employee with the previous and next salary.
        public static IEnumerable<EmployeeLagLeadResult> GetEmployeesWithLagAndLead()
        {
            var orderedEmployees = SeedData.Employees
                .OrderBy(e => e.HireDate)
                .ThenBy(e => e.Name)
                .ToList();

            var previousSalaries = orderedEmployees
                .Select(e => (decimal?)e.Salary)
                .Prepend(null);

            var nextSalaries = orderedEmployees
                .Select(e => (decimal?)e.Salary)
                .Append(null)
                .Skip(1);

            return orderedEmployees
                .Zip(previousSalaries, nextSalaries)
                .Select(x => new EmployeeLagLeadResult(
                    x.First.Name,
                    x.First.Salary,
                    x.Second,
                    x.Third));
        }





        //  Result model for the highest-paid employees in each department.
        public record TopEmployeePerDepartmentResult(
            string DepartmentName,
            string EmployeeName,
            decimal Salary);

        // Returns the top N highest-paid employees from each department.
        // MaxBy is used when requesting the single highest-paid employee per department.
        public static IEnumerable<TopEmployeePerDepartmentResult> GetTopEmployeesPerDepartment(int topN)
        {
            if (topN < 1)
                throw new ArgumentOutOfRangeException(nameof(topN));

            var departments = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new
                    {
                        DepartmentName = department.Name,
                        Employees = group
                    });

            if (topN == 1)
            {
                return departments
                    .Select(group =>
                    {
                        var employee = group.Employees.MaxBy(e => e.Salary)!;

                        return new TopEmployeePerDepartmentResult(
                            group.DepartmentName,
                            employee.Name,
                            employee.Salary);
                    });
            }

            return departments
                .SelectMany(group =>
                    group.Employees
                        .OrderByDescending(e => e.Salary)
                        .ThenBy(e => e.Name)
                        .Take(topN)
                        .Select(employee => new TopEmployeePerDepartmentResult(
                            group.DepartmentName,
                            employee.Name,
                            employee.Salary)));
        }


    }
}