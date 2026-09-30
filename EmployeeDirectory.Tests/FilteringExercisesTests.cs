using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;
using EmployeeDirectory.Exercises;

namespace EmployeeDirectory.Tests
{
    public class FilteringExercisesTests
    {
        // Tests Where + Select for high-salary employees.
        [Fact]
        public void GetHighSalaryEmployees_ShouldReturnEmployeesAbove12000()
        {
            var result = FilteringExercises.GetHighSalaryEmployees();

            Assert.All(result, employee => Assert.True(employee.Salary > 12000));
        }

        //  Tests Select with zero-based index.
        [Fact]
        public void GetEmployeesWithIndex_ShouldReturnZeroBasedIndexes()
        {
            var result = FilteringExercises.GetEmployeesWithIndex().ToList();

            Assert.Equal(0, result.First().Index);
            Assert.Equal("Ahmed Hassan", result.First().Name);
            Assert.Equal(24, result.Last().Index);
        }

        // - Tests employee and department projection using a named record.
        [Fact]
        public void GetEmployeeSummaries_ShouldReturnEmployeeAndDepartmentNames()
        {
            var result = FilteringExercises.GetEmployeeSummaries().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.DepartmentName == "Frontend");

            Assert.Contains(result, x =>
                x.EmployeeName == "Mona Ali" &&
                x.DepartmentName == "Backend");
        }

        //  Tests DistinctBy for unique salaries.
        [Fact]
        public void GetEmployeesWithDistinctSalaries_ShouldReturnUniqueSalaries()
        {
            var result = FilteringExercises.GetEmployeesWithDistinctSalaries().ToList();

            Assert.Equal(
                result.Select(e => e.Salary).Distinct().Count(),
                result.Count);
        }

        //  Tests OrderBy salary and ThenBy name.
        [Fact]
        public void GetEmployeesOrderedBySalary_ShouldReturnSortedEmployees()
        {
            var result = OrderingExercises.GetEmployeesOrderedBySalary().ToList();

            Assert.Equal(8500, result.First().Salary);
            Assert.Equal(16000, result.Last().Salary);

            Assert.True(
                result.Zip(result.Skip(1))
                    .All(pair =>
                        pair.First.Salary < pair.Second.Salary ||
                        (pair.First.Salary == pair.Second.Salary &&
                         string.Compare(pair.First.Name, pair.Second.Name, StringComparison.Ordinal) <= 0)));
        }

        //  Tests pagination using Skip and Take.
        [Fact]
        public void GetPage_ShouldReturnCorrectPage()
        {
            var result = OrderingExercises.GetPage(2, 5).ToList();

            Assert.Equal(5, result.Count);
            Assert.Equal(6, result.First().Id);
            Assert.Equal(10, result.Last().Id);
        }

        // Tests pagination input validation.
        [Fact]
        public void GetPage_ShouldThrowForInvalidArguments()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => OrderingExercises.GetPage(0, 5).ToList());

            Assert.Throws<ArgumentOutOfRangeException>(
                () => OrderingExercises.GetPage(1, 0).ToList());
        }

        //  Tests splitting employees into chunks.
        [Fact]
        public void GetEmployeeChunks_ShouldSplitEmployeesIntoCorrectChunks()
        {
            var result = OrderingExercises.GetEmployeeChunks(5).ToList();

            Assert.Equal(5, result.Count);
            Assert.All(result, chunk => Assert.Equal(5, chunk.Count()));
        }


        // Tests First, FirstOrDefault, Single, and SingleOrDefault.
        [Fact]
        public void GetEmployeeElementResults_ShouldReturnExpectedResults()
        {
            var result = OrderingExercises.GetEmployeeElementResults();

            Assert.Equal("Ahmed Hassan", result.FirstName);
            Assert.Null(result.FirstOrDefaultName);

            Assert.Equal("Ahmed Hassan", result.SingleName);
            Assert.Null(result.SingleOrDefaultName);
        }

        //  Tests the employees with the highest and lowest salaries.
        [Fact]
        public void GetSalaryExtremes_ShouldReturnHighestAndLowestSalary()
        {
            var result = OrderingExercises.GetSalaryExtremes();

            Assert.Equal("Tarek Mahmoud", result.MaxSalaryEmployee);
            Assert.Equal(16000, result.MaxSalary);

            Assert.Equal("Mai Ahmed", result.MinSalaryEmployee);
            Assert.Equal(8500, result.MinSalary);
        }

        //  Tests joining employees with their departments.
        [Fact]
        public void GetEmployeesWithDepartments_ShouldReturnEmployeeAndDepartmentNames()
        {
            var result = JoinExercises.GetEmployeesWithDepartments().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.DepartmentName == "Frontend");

            Assert.Contains(result, x =>
                x.EmployeeName == "Mona Ali" &&
                x.DepartmentName == "Backend");
        }


        //  Tests the employee-department join using query syntax.
        [Fact]
        public void GetEmployeesWithDepartmentsQuerySyntax_ShouldReturnExpectedResults()
        {
            var result = JoinExercises.GetEmployeesWithDepartmentsQuerySyntax().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.DepartmentName == "Frontend");

            Assert.Contains(result, x =>
                x.EmployeeName == "Mona Ali" &&
                x.DepartmentName == "Backend");
        }


        //  Tests GroupJoin and confirms that empty departments are included.
        [Fact]
        public void GetDepartmentsWithEmployees_ShouldIncludeEmptyDepartment()
        {
            var result = JoinExercises.GetDepartmentsWithEmployees().ToList();

            var emptyDepartment = result
                .Single(x => x.DepartmentName == "Empty Department");

            Assert.Empty(emptyDepartment.EmployeeNames);

            var frontendDepartment = result
                .Single(x => x.DepartmentName == "Frontend");

            Assert.Contains("Ahmed Hassan", frontendDepartment.EmployeeNames);
        }


        //  Tests that the left outer join keeps an employee without a department.
        [Fact]
        public void GetEmployeesWithOptionalDepartments_ShouldIncludeOrphanEmployee()
        {
            var result = JoinExercises.GetEmployeesWithOptionalDepartments().ToList();

            var orphanEmployee = result
                .Single(x => x.EmployeeName == "Orphan Employee");

            Assert.Null(orphanEmployee.DepartmentName);
        }

        //  Tests that the full outer join includes matched and unmatched records.
        [Fact]
        public void GetFullEmployeeDepartmentJoin_ShouldIncludeMatchedAndUnmatchedRecords()
        {
            var result = JoinExercises.GetFullEmployeeDepartmentJoin().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.DepartmentName == "Frontend");

            Assert.Contains(result, x =>
                x.EmployeeName == "Orphan Employee" &&
                x.DepartmentName == null);

            Assert.Contains(result, x =>
                x.EmployeeName == null &&
                x.DepartmentName == "Empty Department");
        }


        // Tests the self join between employees and their managers.
        [Fact]
        public void GetEmployeesWithManagers_ShouldReturnEmployeeManagerPairs()
        {
            var result = JoinExercises.GetEmployeesWithManagers().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Omar Mohamed" &&
                x.ManagerName == "Mona Ali");

            Assert.Contains(result, x =>
                x.EmployeeName == "Nour Khaled" &&
                x.ManagerName == "Ahmed Hassan");
        }


        //  Tests that the cross join returns every department-branch combination.
    
        [Fact]
        public void GetDepartmentBranchCombinations_ShouldReturnAllCombinations()
        {
            var result = JoinExercises.GetDepartmentBranchCombinations().ToList();

            Assert.Equal(
                SeedData.Departments.Count * SeedData.Branches.Count,
                result.Count);

            Assert.Contains(result, x =>
                x.DepartmentName == "Frontend" &&
                x.BranchName == "Cairo Branch");

            Assert.Contains(result, x =>
                x.DepartmentName == "Empty Department" &&
                x.BranchName == "Zagazig Branch");
        }


        //  Tests the many-to-many relationship between employees and skills.
        [Fact]
        public void GetEmployeesWithSkills_ShouldReturnEmployeeSkillPairs()
        {
            var result = JoinExercises.GetEmployeesWithSkills().ToList();

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.SkillName == "C#");

            Assert.Contains(result, x =>
                x.EmployeeName == "Ahmed Hassan" &&
                x.SkillName == "Angular");

            Assert.Contains(result, x =>
                x.EmployeeName == "Mona Ali" &&
                x.SkillName == "SQL");
        }

        //  Tests that Union combines both groups without duplicate employees.
        [Fact]
        public void GetUnionOfEmployeeGroups_ShouldRemoveDuplicates()
        {
            var result = SetOperationsExercises.GetUnionOfEmployeeGroups().ToList();

            var expectedNames = SeedData.Employees
                .Where(e => e.Salary >= 12000 || e.Status == EmploymentStatus.Active)
                .Select(e => e.Name)
                .Distinct()
                .ToHashSet();

            var actualNames = result
                .Select(x => x.Name)
                .ToHashSet();

            Assert.Equal(expectedNames, actualNames);
            Assert.Equal(actualNames.Count, result.Count);
        }

        // Tests that Concat keeps duplicates while Union removes them.
        [Fact]
        public void CompareConcatAndUnion_ShouldShowDifference()
        {
            var result = SetOperationsExercises.CompareConcatAndUnion();

            Assert.True(result.ConcatCount > result.UnionCount);
        }



        // Tests that Intersect returns only employees present in both groups.
        [Fact]
        public void GetIntersectingEmployees_ShouldReturnSharedEmployees()
        {
            var result = SetOperationsExercises.GetIntersectingEmployees().ToList();

            var expectedNames = SeedData.Employees
                .Where(e => e.Salary >= 12000 && e.Status == EmploymentStatus.Active)
                .Select(e => e.Name)
                .ToHashSet();

            var actualNames = result
                .Select(x => x.Name)
                .ToHashSet();

            Assert.Equal(expectedNames, actualNames);
        }


        //  Tests that Except removes employees found in the active group.
        [Fact]
        public void GetEmployeesExceptActive_ShouldReturnExpectedEmployees()
        {
            var result = SetOperationsExercises.GetEmployeesExceptActive().ToList();

            var expectedNames = SeedData.Employees
                .Where(e => e.Salary >= 12000 && e.Status != EmploymentStatus.Active)
                .Select(e => e.Name)
                .ToHashSet();

            var actualNames = result
                .Select(x => x.Name)
                .ToHashSet();

            Assert.Equal(expectedNames, actualNames);
        }

        //  Tests IntersectBy, ExceptBy, and UnionBy using DepartmentId as the key.
        [Fact]
        public void GetDepartmentSetOperationsByKey_ShouldUseDepartmentIdAsKey()
        {
            var result = SetOperationsExercises.GetDepartmentSetOperationsByKey();

            var firstGroup = SeedData.Employees
                .Where(e => e.Id <= 10)
                .Select(e => new { e.DepartmentId, e.Name })
                .ToList();

            var secondGroup = SeedData.Employees
                .Where(e => e.Id > 10 && e.Id <= 20)
                .Select(e => new { e.DepartmentId, e.Name })
                .ToList();

            var secondDepartmentIds = secondGroup
                .Select(e => e.DepartmentId)
                .ToHashSet();

            Assert.All(
                result.IntersectByResult,
                x => Assert.Contains(x.DepartmentId, secondDepartmentIds));

            Assert.All(
                result.ExceptByResult,
                x => Assert.DoesNotContain(x.DepartmentId, secondDepartmentIds));

            Assert.Equal(
                firstGroup.Select(e => e.DepartmentId)
                    .Concat(secondGroup.Select(e => e.DepartmentId))
                    .Distinct()
                    .Count(),
                result.UnionByResult.Count());
        }


        //  Tests that the custom comparer ignores letter casing.
        [Fact]
        public void GetDistinctNamesIgnoringCase_ShouldRemoveCaseDuplicates()
        {
            var result = SetOperationsExercises
                .GetDistinctNamesIgnoringCase()
                .ToList();

            Assert.Equal(3, result.Count);

            Assert.Contains("Ahmed Hassan", result);
            Assert.Contains("Mona Ali", result);
            Assert.Contains("Omar Mohamed", result);
        }


        //GroupingExercises.cs 

        //  Tests department grouping and salary aggregates.
        [Fact]
        public void GetDepartmentSalaryAggregates_ShouldReturnCorrectAggregates()
        {
            var result = GroupingExercises
                .GetDepartmentSalaryAggregates()
                .ToList();

            var expected = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new
                    {
                        DepartmentName = department.Name,
                        EmployeeCount = group.Count(),
                        AverageSalary = group.Average(e => e.Salary),
                        MaxSalary = group.Max(e => e.Salary),
                        MinSalary = group.Min(e => e.Salary)
                    })
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedDepartment = expected
                    .Single(e => e.DepartmentName == actual.DepartmentName);

                Assert.Equal(expectedDepartment.EmployeeCount, actual.EmployeeCount);
                Assert.Equal(expectedDepartment.AverageSalary, actual.AverageSalary);
                Assert.Equal(expectedDepartment.MaxSalary, actual.MaxSalary);
                Assert.Equal(expectedDepartment.MinSalary, actual.MinSalary);
            });
        }


        //  Tests HAVING-like filtering by keeping departments with at least 3 employees.
        [Fact]
        public void GetDepartmentsWithAtLeastThreeEmployees_ShouldFilterGroups()
        {
            var result = GroupingExercises
                .GetDepartmentsWithAtLeastThreeEmployees()
                .ToList();

            var expected = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Where(group => group.Count() >= 3)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new
                    {
                        DepartmentName = department.Name,
                        EmployeeCount = group.Count()
                    })
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedDepartment = expected
                    .Single(e => e.DepartmentName == actual.DepartmentName);

                Assert.True(actual.EmployeeCount >= 3);
                Assert.Equal(expectedDepartment.EmployeeCount, actual.EmployeeCount);
            });
        }



        //  Tests grouping employees by both department and employment status.
        [Fact]
        public void GetEmployeesGroupedByDepartmentAndStatus_ShouldGroupByCompositeKey()
        {
            var result = GroupingExercises
                .GetEmployeesGroupedByDepartmentAndStatus()
                .ToList();

            var expected = SeedData.Employees
                .GroupBy(e => new
                {
                    e.DepartmentId,
                    e.Status
                })
                .Select(group => new
                {
                    group.Key.DepartmentId,
                    group.Key.Status,
                    EmployeeCount = group.Count()
                })
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedGroup = expected.Single(e =>
                    e.DepartmentId == actual.DepartmentId &&
                    e.Status == actual.Status);

                Assert.Equal(expectedGroup.EmployeeCount, actual.EmployeeCount);
            });
        }


        // Tests salary statistics calculated using Aggregate.
        [Fact]
        public void GetSalaryStatistics_ShouldReturnCorrectStatistics()
        {
            var result = GroupingExercises.GetSalaryStatistics();

            var expectedCount = SeedData.Employees.Count;
            var expectedTotal = SeedData.Employees.Sum(e => e.Salary);
            var expectedAverage = expectedTotal / expectedCount;
            var expectedMax = SeedData.Employees.Max(e => e.Salary);
            var expectedMin = SeedData.Employees.Min(e => e.Salary);

            Assert.Equal(expectedCount, result.EmployeeCount);
            Assert.Equal(expectedTotal, result.TotalSalary);
            Assert.Equal(expectedAverage, result.AverageSalary);
            Assert.Equal(expectedMax, result.MaxSalary);
            Assert.Equal(expectedMin, result.MinSalary);
        }


        //  Tests the running salary total for every employee.
        [Fact]
        public void GetRunningSalaryTotals_ShouldCalculateRunningTotal()
        {
            var result = GroupingExercises
                .GetRunningSalaryTotals()
                .ToList();

            var expectedRunningTotal = 0m;

            Assert.Equal(SeedData.Employees.Count, result.Count);

            for (var i = 0; i < SeedData.Employees.Count; i++)
            {
                expectedRunningTotal += SeedData.Employees[i].Salary;

                Assert.Equal(SeedData.Employees[i].Name, result[i].EmployeeName);
                Assert.Equal(SeedData.Employees[i].Salary, result[i].Salary);
                Assert.Equal(expectedRunningTotal, result[i].RunningTotal);
            }
        }




        //  Tests that ToLookup, GroupBy, and ToDictionary produce the same number of department groups.
        [Fact]
        public void CompareGroupingMethods_ShouldReturnSameGroupCount()
        {
            var result = GroupingExercises.CompareGroupingMethods();

            var expectedGroupCount = SeedData.Employees
                .Select(e => e.DepartmentId)
                .Distinct()
                .Count();

            Assert.Equal(expectedGroupCount, result.LookupCount);
            Assert.Equal(expectedGroupCount, result.GroupByCount);
            Assert.Equal(expectedGroupCount, result.DictionaryCount);
        }



        //  Tests detail rows, department subtotals, and the grand total.
        [Fact]
        public void GetDepartmentStatusRollup_ShouldReturnDetailsSubtotalsAndGrandTotal()
        {
            var result = GroupingExercises
                .GetDepartmentStatusRollup()
                .ToList();

            var detailCount = SeedData.Employees
                .GroupBy(e => new
                {
                    e.DepartmentId,
                    e.Status
                })
                .Count();

            var departmentCount = SeedData.Employees
                .Select(e => e.DepartmentId)
                .Distinct()
                .Count();

            var expectedTotalRows = detailCount + departmentCount + 1;

            Assert.Equal(expectedTotalRows, result.Count);

            var grandTotal = result.Single(x =>
                x.DepartmentName == null &&
                x.Status == null);

            Assert.Equal(SeedData.Employees.Count, grandTotal.EmployeeCount);

            var subtotalRows = result
                .Where(x =>
                    x.DepartmentName != null &&
                    x.Status == null)
                .ToList();

            Assert.Equal(departmentCount, subtotalRows.Count);

            Assert.All(subtotalRows, subtotal =>
            {
                var expectedCount = SeedData.Employees
                    .Count(e =>
                        SeedData.Departments
                            .Single(d => d.Id == e.DepartmentId)
                            .Name == subtotal.DepartmentName);

                Assert.Equal(expectedCount, subtotal.EmployeeCount);
            });

            var detailRows = result
                .Where(x =>
                    x.DepartmentName != null &&
                    x.Status.HasValue)
                .ToList();

            Assert.Equal(detailCount, detailRows.Count);

            Assert.All(detailRows, detail =>
            {
                var expectedCount = SeedData.Employees
                    .Count(e =>
                        SeedData.Departments
                            .Single(d => d.Id == e.DepartmentId)
                            .Name == detail.DepartmentName &&
                        e.Status == detail.Status);

                Assert.Equal(expectedCount, detail.EmployeeCount);
            });
        }




        //  Tests row numbers after ordering employees by salary and name.
        [Fact]
        public void GetEmployeesWithRowNumber_ShouldAssignSequentialNumbers()
        {
            var result = GroupingExercises
                .GetEmployeesWithRowNumber()
                .ToList();

            var expected = SeedData.Employees
                .OrderBy(e => e.Salary)
                .ThenBy(e => e.Name)
                .Select((employee, index) => new
                {
                    RowNumber = index + 1,
                    employee.Name,
                    employee.Salary
                })
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedEmployee = expected
                    .Single(e => e.RowNumber == actual.RowNumber);

                Assert.Equal(expectedEmployee.Name, actual.EmployeeName);
                Assert.Equal(expectedEmployee.Salary, actual.Salary);
            });

            Assert.Equal(1, result.First().RowNumber);
            Assert.Equal(result.Count, result.Last().RowNumber);
        }




        //  Tests that equal salaries receive the same rank and that rank gaps are preserved.
        [Fact]
        public void GetEmployeesWithRank_ShouldAssignCorrectRanks()
        {
            var result = GroupingExercises
                .GetEmployeesWithRank()
                .ToList();

            var expected = SeedData.Employees
                .OrderByDescending(e => e.Salary)
                .ThenBy(e => e.Name)
                .Select((employee, index) => new
                {
                    Rank = SeedData.Employees
                        .Count(e => e.Salary > employee.Salary) + 1,
                    employee.Name,
                    employee.Salary
                })
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedEmployee = expected
                    .Single(e => e.Name == actual.EmployeeName);

                Assert.Equal(expectedEmployee.Rank, actual.Rank);
                Assert.Equal(expectedEmployee.Salary, actual.Salary);
            });

            var duplicateSalaryGroups = result
                .GroupBy(e => e.Salary)
                .Where(group => group.Count() > 1);

            Assert.All(duplicateSalaryGroups, group =>
            {
                Assert.Single(group.Select(e => e.Rank).Distinct());
            });
        }





        //  Tests that equal salaries share the same dense rank and that there are no gaps.
        [Fact]
        public void GetEmployeesWithDenseRank_ShouldAssignCorrectRanks()
        {
            var result = GroupingExercises
                .GetEmployeesWithDenseRank()
                .ToList();

            var orderedSalaries = SeedData.Employees
                .Select(e => e.Salary)
                .Distinct()
                .OrderByDescending(salary => salary)
                .ToList();

            Assert.Equal(SeedData.Employees.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedRank = orderedSalaries.IndexOf(actual.Salary) + 1;

                Assert.Equal(expectedRank, actual.DenseRank);
            });

            var ranks = result
                .Select(e => e.DenseRank)
                .Distinct()
                .OrderBy(rank => rank)
                .ToList();

            Assert.Equal(
                Enumerable.Range(1, ranks.Count),
                ranks);
        }


        //  Tests that employees are divided into four salary quartiles.
        [Fact]
        public void GetEmployeesBySalaryQuartile_ShouldCreateFourQuartiles()
        {
            var result = GroupingExercises
                .GetEmployeesBySalaryQuartile()
                .ToList();

            Assert.Equal(SeedData.Employees.Count, result.Count);

            Assert.All(result, employee =>
            {
                Assert.InRange(employee.Quartile, 1, 4);
            });

            var quartiles = result
                .GroupBy(employee => employee.Quartile)
                .OrderBy(group => group.Key)
                .ToList();

            Assert.Equal(4, quartiles.Count);

            var largestGroupSize = quartiles.Max(group => group.Count());
            var smallestGroupSize = quartiles.Min(group => group.Count());

            Assert.True(largestGroupSize - smallestGroupSize <= 1);

            Assert.True(
                result
                    .Where(employee => employee.Quartile == 1)
                    .All(employee => employee.Salary >=
                        result
                            .Where(e => e.Quartile == 2)
                            .Max(e => e.Salary)));

            Assert.True(
                result
                    .Where(employee => employee.Quartile == 4)
                    .All(employee => employee.Salary <=
                        result
                            .Where(e => e.Quartile == 3)
                            .Min(e => e.Salary)));
        }


        // Tests previous and next salaries using the employee hire-date order.
        [Fact]
        public void GetEmployeesWithLagAndLead_ShouldReturnPreviousAndNextSalaries()
        {
            var result = GroupingExercises
                .GetEmployeesWithLagAndLead()
                .ToList();

            var expected = SeedData.Employees
                .OrderBy(e => e.HireDate)
                .ThenBy(e => e.Name)
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].EmployeeName);
                Assert.Equal(expected[i].Salary, result[i].Salary);

                decimal? expectedPrevious =
                    i == 0 ? null : expected[i - 1].Salary;

                decimal? expectedNext =
                    i == expected.Count - 1 ? null : expected[i + 1].Salary;

                Assert.Equal(expectedPrevious, result[i].PreviousSalary);
                Assert.Equal(expectedNext, result[i].NextSalary);
            }

            Assert.Null(result.First().PreviousSalary);
            Assert.Null(result.Last().NextSalary);
        }


        //  Tests that the top N highest-paid employees are returned for each department.
        [Fact]
        public void GetTopEmployeesPerDepartment_ShouldReturnTopNEmployeesPerDepartment()
        {
            var result = GroupingExercises
                .GetTopEmployeesPerDepartment(2)
                .ToList();

            var expected = SeedData.Employees
                .GroupBy(e => e.DepartmentId)
                .Join(
                    SeedData.Departments,
                    group => group.Key,
                    department => department.Id,
                    (group, department) => new
                    {
                        DepartmentName = department.Name,
                        Employees = group
                    })
                .SelectMany(group =>
                    group.Employees
                        .OrderByDescending(e => e.Salary)
                        .ThenBy(e => e.Name)
                        .Take(2)
                        .Select(employee => new
                        {
                            group.DepartmentName,
                            employee.Name,
                            employee.Salary
                        }))
                .ToList();

            Assert.Equal(expected.Count, result.Count);

            Assert.All(result, actual =>
            {
                var expectedEmployee = expected
                    .Single(e =>
                        e.DepartmentName == actual.DepartmentName &&
                        e.Name == actual.EmployeeName);

                Assert.Equal(expectedEmployee.Salary, actual.Salary);
            });

            var departmentGroups = result
                .GroupBy(e => e.DepartmentName);

            Assert.All(departmentGroups, group =>
            {
                Assert.InRange(group.Count(), 1, 2);
            });
        }


        //QuantifierExercises.cs


        // Tests Any and All conditions against the employee seed data.
        [Fact]
        public void CheckEmployeeConditions_ShouldReturnCorrectAnyAndAllResults()
        {
            var result = QuantifierExercises.CheckEmployeeConditions();

            var expectedHasHighSalaryEmployee = SeedData.Employees
                .Any(e => e.Salary > 15000);

            var expectedAllEmployeesHavePositiveSalary = SeedData.Employees
                .All(e => e.Salary > 0);

            Assert.Equal(
                expectedHasHighSalaryEmployee,
                result.HasHighSalaryEmployee);

            Assert.Equal(
                expectedAllEmployeesHavePositiveSalary,
                result.AllEmployeesHavePositiveSalary);

            Assert.True(result.HasHighSalaryEmployee);
            Assert.True(result.AllEmployeesHavePositiveSalary);
        }



        //  Tests Contains with a HashSet of required skill IDs.
        [Fact]
        public void GetEmployeesWithRequiredSkills_ShouldDetectMatchingSkills()
        {
            var result = QuantifierExercises
                .GetEmployeesWithRequiredSkills()
                .ToList();

            Assert.Equal(SeedData.Employees.Count, result.Count);

            Assert.All(result, employee =>
            {
                var expected = SeedData.EmployeeSkills
                    .Where(es =>
                        es.EmployeeId ==
                        SeedData.Employees.Single(e => e.Name == employee.EmployeeName).Id)
                    .Select(es => es.SkillId)
                    .Any(skillId => new HashSet<int> { 1, 2, 3 }.Contains(skillId));

                Assert.Equal(expected, employee.HasRequiredSkill);
            });

            Assert.True(
                result.Single(e => e.EmployeeName == "Ahmed Hassan")
                    .HasRequiredSkill);
        }




        // Tests that the naive and dictionary approaches return the same employees.
        [Fact]
        public void GetEmployeesAboveDepartmentAverage_ShouldReturnSameResults()
        {
            var naiveResult = QuantifierExercises
                .GetEmployeesAboveDepartmentAverageNaive()
                .ToList();

            var dictionaryResult = QuantifierExercises
                .GetEmployeesAboveDepartmentAverageDictionary()
                .ToList();

            var naive = naiveResult
                .Select(e => (e.EmployeeName, e.DepartmentId, e.Salary, e.DepartmentAverage))
                .OrderBy(e => e.EmployeeName)
                .ToList();

            var dictionary = dictionaryResult
                .Select(e => (e.EmployeeName, e.DepartmentId, e.Salary, e.DepartmentAverage))
                .OrderBy(e => e.EmployeeName)
                .ToList();

            Assert.Equal(naive, dictionary);

            Assert.All(naiveResult, employee =>
            {
                Assert.True(employee.Salary > employee.DepartmentAverage);
            });
        }



        //  Tests that returned employees do not have any of the top three skills.
        [Fact]
        public void GetEmployeesWithoutTopThreeSkills_ShouldExcludeEmployeesWithTopSkills()
        {
            var result = QuantifierExercises
                .GetEmployeesWithoutTopThreeSkills()
                .ToList();

            var topThreeSkillIds = SeedData.EmployeeSkills
                .GroupBy(es => es.SkillId)
                .OrderByDescending(group => group.Count())
                .ThenBy(group => group.Key)
                .Take(3)
                .Select(group => group.Key)
                .ToHashSet();

            Assert.All(result, employee =>
            {
                var employeeId = SeedData.Employees
                    .Single(e => e.Name == employee.EmployeeName)
                    .Id;

                var hasTopSkill = SeedData.EmployeeSkills
                    .Where(es => es.EmployeeId == employeeId)
                    .Any(es => topThreeSkillIds.Contains(es.SkillId));

                Assert.False(hasTopSkill);
            });

            var expected = SeedData.Employees
                .Where(employee =>
                    !SeedData.EmployeeSkills
                        .Where(es => es.EmployeeId == employee.Id)
                        .Any(es => topThreeSkillIds.Contains(es.SkillId)))
                .Select(employee => employee.Name)
                .OrderBy(name => name)
                .ToList();

            var actual = result
                .Select(employee => employee.EmployeeName)
                .OrderBy(name => name)
                .ToList();

            Assert.Equal(expected, actual);
        }




        //  Tests employees whose salary is above the company-wide average.
        [Fact]
        public void GetEmployeesAboveCompanyAverage_ShouldReturnCorrectEmployees()
        {
            var result = QuantifierExercises
                .GetEmployeesAboveCompanyAverage()
                .ToList();

            var companyAverage = SeedData.Employees
                .Average(e => e.Salary);

            var expected = SeedData.Employees
                .Where(e => e.Salary > companyAverage)
                .Select(e => new
                {
                    EmployeeName = e.Name,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeName)
                .ToList();

            var actual = result
                .Select(e => new
                {
                    EmployeeName = e.EmployeeName,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeName)
                .ToList();

            Assert.Equal(expected, actual);

            Assert.All(result, employee =>
            {
                Assert.Equal(companyAverage, employee.CompanyAverageSalary);
                Assert.True(employee.Salary > employee.CompanyAverageSalary);
            });
        }




        //  Tests that direct and indirect descendants are returned recursively.
        [Fact]
        public void GetDescendants_ShouldReturnAllDirectAndIndirectEmployees()
        {
            var managerId = 1;

            var result = QuantifierExercises
                .GetDescendants(managerId)
                .ToList();

            var expected = SeedData.Employees
                .Where(e => e.ManagerId == managerId)
                .Select(e => e.Name)
                .ToHashSet();

            var directEmployees = SeedData.Employees
                .Where(e => e.ManagerId == managerId)
                .ToList();

            var allExpectedNames = new HashSet<string>(expected);

            foreach (var employee in directEmployees)
            {
                var descendants = SeedData.Employees
                    .Where(e => e.ManagerId == employee.Id)
                    .Select(e => e.Name);

                allExpectedNames.UnionWith(descendants);
            }

            Assert.Equal(
                allExpectedNames,
                result.Select(e => e.EmployeeName).ToHashSet());

            Assert.All(result, employee =>
            {
                Assert.Contains(SeedData.Employees, e => e.Id == employee.EmployeeId);
            });
        }

        //  Tests that the manager and all direct and indirect employees are returned.
        [Fact]
        public void GetEveryoneUnderManager_ShouldReturnManagerAndAllDescendants()
        {
            var managerId = 1;

            var result = QuantifierExercises
                .GetEveryoneUnderManager(managerId)
                .ToList();

            var expectedIds = new HashSet<int>
    {
        managerId
    };

            void CollectDescendants(int currentManagerId)
            {
                foreach (var employee in SeedData.Employees
                    .Where(e => e.ManagerId == currentManagerId))
                {
                    expectedIds.Add(employee.Id);
                    CollectDescendants(employee.Id);
                }
            }

            CollectDescendants(managerId);

            Assert.Equal(
                expectedIds,
                result.Select(e => e.EmployeeId).ToHashSet());

            Assert.Contains(
                result,
                employee => employee.EmployeeId == managerId);

            Assert.All(result, employee =>
            {
                Assert.Contains(employee.EmployeeId, expectedIds);
            });
        }


        // Tests that each employee receives the correct management path.
        [Fact]
        public void GetEmployeeManagementPaths_ShouldBuildCorrectPaths()
        {
            var result = QuantifierExercises
                .GetEmployeeManagementPaths()
                .ToList();

            Assert.Equal(SeedData.Employees.Count, result.Count);

            foreach (var employee in SeedData.Employees)
            {
                var actual = result
                    .Single(e => e.EmployeeName == employee.Name);

                var path = new List<string>();
                var currentEmployee = employee;

                path.Add(currentEmployee.Name);

                while (currentEmployee.ManagerId.HasValue)
                {
                    currentEmployee = SeedData.Employees
                        .Single(e => e.Id == currentEmployee.ManagerId.Value);

                    path.Add(currentEmployee.Name);
                }

                path.Reverse();

                var expectedPath = string.Join(" → ", path);

                Assert.Equal(expectedPath, actual.ManagementPath);
            }
        }

        // B.8.4 - Tests that a valid hierarchy still produces the correct management path.
        [Fact]
        public void GetSafeEmployeeManagementPaths_ShouldHandleValidHierarchy()
        {
            var employees = new[]
            {
        new Employee
        {
            Id = 1,
            Name = "Ahmed",
            ManagerId = null
        },
        new Employee
        {
            Id = 2,
            Name = "Mona",
            ManagerId = 1
        },
        new Employee
        {
            Id = 3,
            Name = "Omar",
            ManagerId = 2
        }
    };

            var result = QuantifierExercises
                .GetSafeEmployeeManagementPaths(employees)
                .ToList();

            var omar = result.Single(e => e.EmployeeName == "Omar");

            Assert.Equal(
                "Ahmed → Mona → Omar",
                omar.ManagementPath);
        }

        //  Tests that a circular manager relationship throws an exception.
        [Fact]
        public void GetSafeEmployeeManagementPaths_ShouldThrowWhenCycleExists()
        {
            var employees = new[]
            {
        new Employee
        {
            Id = 1,
            Name = "Ahmed",
            ManagerId = 3
        },
        new Employee
        {
            Id = 2,
            Name = "Mona",
            ManagerId = 1
        },
        new Employee
        {
            Id = 3,
            Name = "Omar",
            ManagerId = 2
        }
    };

            var exception = Assert.Throws<InvalidOperationException>(() =>
                QuantifierExercises
                    .GetSafeEmployeeManagementPaths(employees)
                    .ToList());

            Assert.Contains(
                "Cycle detected",
                exception.Message);
        }

        //  Tests that deferred execution sees source changes while immediate execution does not.
        [Fact]
        public void DemonstrateDeferredAndImmediateExecution_ShouldShowExecutionDifference()
        {
            var result = QuantifierExercises
                .DemonstrateDeferredAndImmediateExecution();

            Assert.Equal(25, result.OriginalEmployeeCount);

            Assert.Equal(
                result.ImmediateResultCount + 1,
                result.DeferredResultCount);

            Assert.Equal(
                result.DeferredResultCount,
                result.DeferredResultCountAfterSourceChange);
        }


        //  Tests that repeated enumeration returns the same data
        // and that materialized results can be reused.
        [Fact]
        public void DemonstrateMultipleEnumeration_ShouldReturnConsistentResults()
        {
            var result = QuantifierExercises
                .DemonstrateMultipleEnumeration();

            Assert.Equal(
                result.FirstEnumerationCount,
                result.SecondEnumerationCount);

            Assert.Equal(
                result.FirstEnumerationCount,
                result.MaterializedCount);

            Assert.Equal(
                result.MaterializedCount,
                result.MaterializedCountAgain);
        }



        //  Tests performance measurement using 100,000 generated employees.
        [Fact]
        public void MeasurePerformance_ShouldProcess100000Employees()
        {
            var result = QuantifierExercises.MeasurePerformance();

            Assert.Equal(100_000, result.EmployeeCount);

            Assert.True(result.MatchingEmployeeCount > 0);

            Assert.True(result.QueryMilliseconds >= 0);

            Assert.True(result.MaterializationMilliseconds >= 0);
        }


        //EmployeeExtensions.cs

        // Tests the Employee extension methods.
        [Fact]
        public void EmployeeExtensions_ShouldReturnYearsOfServiceAndCurrentStatus()
        {
            var employee = SeedData.Employees.First();

            var expectedYears =
                DateTime.Today.Year - employee.HireDate.Year;

            if (employee.HireDate >
                DateOnly.FromDateTime(DateTime.Today).AddYears(expectedYears))
            {
                expectedYears--;
            }

            Assert.Equal(
                expectedYears,
                employee.YearsOfService());

            Assert.Equal(
                employee.Status,
                employee.CurrentStatus());
        }


        //EnumerableExtensions.cs

        //  Tests that WhereIf applies the predicate only when the condition is true.
        [Fact]
        public void WhereIf_ShouldApplyPredicateOnlyWhenConditionIsTrue()
        {
            var filtered = SeedData.Employees
                .WhereIf(true, e => e.Salary > 12000)
                .ToList();

            var unfiltered = SeedData.Employees
                .WhereIf(false, e => e.Salary > 12000)
                .ToList();

            Assert.All(filtered, employee =>
            {
                Assert.True(employee.Salary > 12000);
            });

            Assert.Equal(
                SeedData.Employees.Count,
                unfiltered.Count);

            Assert.Equal(
                SeedData.Employees.Count(e => e.Salary > 12000),
                filtered.Count);
        }




        //  Tests the lazy Pairwise extension method.
        [Fact]
        public void Pairwise_ShouldReturnAdjacentPairs()
        {
            var numbers = new[] { 10, 20, 30, 40 };

            var result = numbers
                .Pairwise()
                .ToList();

            var expected = new[]
            {
        (Current: 10, Next: 20),
        (Current: 20, Next: 30),
        (Current: 30, Next: 40)
    };

            Assert.Equal(expected, result);
        }


        //  Tests that Func and Expression<Func> produce the same result.
        [Fact]
        public void CompareFuncAndExpression_ShouldReturnSameResults()
        {
            var numbers = Enumerable.Range(1, 10);

            Func<int, bool> funcPredicate =
                number => number > 5;

            System.Linq.Expressions.Expression<Func<int, bool>> expressionPredicate =
                number => number > 5;

            var result = EnumerableExtensions.CompareFuncAndExpression(
                numbers,
                funcPredicate,
                expressionPredicate);

            Assert.True(result);
        }


    }
}