using EmployeeDirectory.Data;
using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Exercises
{
    public static class OrderingExercises
    {
        public record EmployeeOrderResult(string Name, decimal Salary);
        public record EmployeePageResult(int Id, string Name);

        //  OrderBy + ThenBy
        // Orders employees by salary ascending.
        // If two employees have the same salary, orders them by name ascending.
        public static IEnumerable<EmployeeOrderResult> GetEmployeesOrderedBySalary()
        {
            return SeedData.Employees
                .OrderBy(e => e.Salary)
                .ThenBy(e => e.Name)
                .Select(e => new EmployeeOrderResult(e.Name, e.Salary));
        }

        // Skip + Take / Pagination
        // Returns one page of employees using Skip and Take.
        // pageNumber and pageSize must be greater than zero.
        public static IEnumerable<EmployeePageResult> GetPage(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return SeedData.Employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeePageResult(e.Id, e.Name));
        }

        // Chunk
        // Splits the employees into smaller groups with the specified chunk size.
        // The last chunk can contain fewer employees if the total count is not divisible by the chunk size.
        public static IEnumerable<IEnumerable<Employee>> GetEmployeeChunks(int chunkSize)
        {
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException(nameof(chunkSize));

            return SeedData.Employees.Chunk(chunkSize);
        }


        //  Result model for First, FirstOrDefault, Single, and SingleOrDefault.
        public record EmployeeElementResult(
            string FirstName,
            string? FirstOrDefaultName,
            string SingleName,
            string? SingleOrDefaultName);

        //  Demonstrates First, FirstOrDefault, Single, and SingleOrDefault.
        public static EmployeeElementResult GetEmployeeElementResults()
        {
            var first = SeedData.Employees.First();
            var firstOrDefault = SeedData.Employees.FirstOrDefault(e => e.Salary == 999999);

            var single = SeedData.Employees.Single(e => e.Id == 1);
            var singleOrDefault = SeedData.Employees.SingleOrDefault(e => e.Id == 999);

            return new EmployeeElementResult(
                first.Name,
                firstOrDefault?.Name,
                single.Name,
                singleOrDefault?.Name);
        }

        //  Result model for maximum and minimum salary employees.
        public record EmployeeSalaryExtremesResult(string MaxSalaryEmployee, decimal MaxSalary, string MinSalaryEmployee, decimal MinSalary);


        //  Finds the employees with the highest and lowest salaries using MaxBy and MinBy.
        public static EmployeeSalaryExtremesResult GetSalaryExtremes()
        {
            var maxEmployee = SeedData.Employees.MaxBy(e => e.Salary)!;
            var minEmployee = SeedData.Employees.MinBy(e => e.Salary)!;

            return new EmployeeSalaryExtremesResult(
                maxEmployee.Name,
                maxEmployee.Salary,
                minEmployee.Name,
                minEmployee.Salary);
        }





    }



}