using EmployeeDirectory.Data;
using EmployeeDirectory.Exercises;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeDirectory.Tests
{
    public class DatabaseExercisesTests
    {
        [Fact]
        public void GetHighSalaryEmployeesFromDatabase_ShouldReturnResultsAndSql()
        {
            var options = new DbContextOptionsBuilder<EmployeeDirectoryDbContext>()
                .UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;" +
                    "Database=EmployeeDirectoryDb;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;")
                .Options;

            using var context = new EmployeeDirectoryDbContext(options);

            var result = DatabaseExercises
                .GetHighSalaryEmployeesFromDatabase(context);

            Assert.NotNull(result);
            Assert.NotNull(result.Sql);
            Assert.NotEmpty(result.Sql);
        }
    }
}