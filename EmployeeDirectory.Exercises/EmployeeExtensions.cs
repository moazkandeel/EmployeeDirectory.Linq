using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Exercises
{
    public static class EmployeeExtensions
    {
        // B.10.1 - Returns the completed number of years
        // the employee has worked based on the HireDate.
        public static int YearsOfService(this Employee employee)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var years = today.Year - employee.HireDate.Year;

            if (employee.HireDate > today.AddYears(-years))
                years--;

            return years;
        }

        // Returns the employee's current employment status.
        public static EmploymentStatus CurrentStatus(this Employee employee)
        {
            return employee.Status;
        }










    }
}