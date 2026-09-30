using EmployeeDirectory.Domain;

namespace EmployeeDirectory.Data
{
    public static class SeedData
    {
        public static IReadOnlyList<Department> Departments { get; } =
        [
            new Department
            {
                Id = 1,
                Name = "Technology",
                ParentDepartmentId = null
            },
            new Department
            {
                Id = 2,
                Name = "Human Resources",
                ParentDepartmentId = null
            },
            new Department
            {
                Id = 3,
                Name = "Frontend",
                ParentDepartmentId = 1
            },
            new Department
            {
                Id = 4,
                Name = "Backend",
                ParentDepartmentId = 1
            },
            new Department
            {
                Id = 5,
                Name = "Finance",
                ParentDepartmentId = null
            },
            new Department
            {
                Id = 6,
                Name = "Empty Department",
                ParentDepartmentId = null
            }
        ];

        public static IReadOnlyList<JobRole> JobRoles { get; } =
        [
            new JobRole
            {
                Id = 1,
                Title = "Frontend Developer",
                DepartmentId = 3
            },
            new JobRole
            {
                Id = 2,
                Title = "Backend Developer",
                DepartmentId = 4
            },
            new JobRole
            {
                Id = 3,
                Title = "QA Engineer",
                DepartmentId = 1
            },
            new JobRole
            {
                Id = 4,
                Title = "HR Specialist",
                DepartmentId = 2
            },
            new JobRole
            {
                Id = 5,
                Title = "Accountant",
                DepartmentId = 5
            },
            new JobRole
            {
                Id = 6,
                Title = "Database Developer",
                DepartmentId = 4
            }
        ];

        public static IReadOnlyList<Branch> Branches { get; } =
        [
            new Branch
            {
                Id = 1,
                Name = "Cairo Branch",
                City = "Cairo"
            },
            new Branch
            {
                Id = 2,
                Name = "Zagazig Branch",
                City = "Zagazig"
            },
            new Branch
            {
                Id = 3,
                Name = "Alexandria Branch",
                City = "Alexandria"
            }
        ];

        public static IReadOnlyList<Skill> Skills { get; } =
        [
            new Skill { Id = 1, Name = "C#" },
            new Skill { Id = 2, Name = "SQL" },
            new Skill { Id = 3, Name = "LINQ" },
            new Skill { Id = 4, Name = "Angular" },
            new Skill { Id = 5, Name = "React" },
            new Skill { Id = 6, Name = "ASP.NET Core" },
            new Skill { Id = 7, Name = "Git" },
            new Skill { Id = 8, Name = "Docker" },
            new Skill { Id = 9, Name = "JavaScript" },
            new Skill { Id = 10, Name = "TypeScript" }
        ];

        public static IReadOnlyList<Employee> Employees { get; } =
        [
            new Employee
            {
                Id = 1,
                Name = "Ahmed Hassan",
                Salary = 12000,
                HireDate = new DateOnly(2023, 1, 15),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 1,
                ManagerId = null,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 2,
                Name = "Mona Ali",
                Salary = 15000,
                HireDate = new DateOnly(2022, 6, 10),
                DepartmentId = 4,
                JobRoleId = 2,
                BranchId = 1,
                ManagerId = null,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 3,
                Name = "Omar Mohamed",
                Salary = 10000,
                HireDate = new DateOnly(2024, 3, 20),
                DepartmentId = 1,
                JobRoleId = 3,
                BranchId = 2,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 4,
                Name = "Sara Mahmoud",
                Salary = 9000,
                HireDate = new DateOnly(2024, 5, 5),
                DepartmentId = 2,
                JobRoleId = 4,
                BranchId = 2,
                ManagerId = null,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 5,
                Name = "Youssef Adel",
                Salary = 11000,
                HireDate = new DateOnly(2023, 9, 1),
                DepartmentId = 5,
                JobRoleId = 5,
                BranchId = 3,
                ManagerId = 4,
                Status = EmploymentStatus.OnLeave
            },
            new Employee
            {
                Id = 6,
                Name = "Nour Khaled",
                Salary = 13000,
                HireDate = new DateOnly(2023, 11, 12),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 1,
                ManagerId = 1,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 7,
                Name = "Karim Samir",
                Salary = 15000,
                HireDate = new DateOnly(2022, 2, 18),
                DepartmentId = 4,
                JobRoleId = 2,
                BranchId = 2,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 8,
                Name = "Hana Tarek",
                Salary = 9500,
                HireDate = new DateOnly(2024, 7, 8),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 3,
                ManagerId = 1,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 9,
                Name = "Mostafa Nabil",
                Salary = 10000,
                HireDate = new DateOnly(2023, 4, 25),
                DepartmentId = 4,
                JobRoleId = 6,
                BranchId = 1,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 10,
                Name = "Laila Hossam",
                Salary = 11000,
                HireDate = new DateOnly(2024, 1, 30),
                DepartmentId = 5,
                JobRoleId = 5,
                BranchId = 3,
                ManagerId = 4,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 11,
                Name = "Dina Ashraf",
                Salary = 14000,
                HireDate = new DateOnly(2022, 10, 5),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 1,
                ManagerId = 1,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 12,
                Name = "Hassan Mostafa",
                Salary = 12500,
                HireDate = new DateOnly(2023, 6, 15),
                DepartmentId = 4,
                JobRoleId = 2,
                BranchId = 2,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 13,
                Name = "Mai Ahmed",
                Salary = 8500,
                HireDate = new DateOnly(2024, 8, 20),
                DepartmentId = 2,
                JobRoleId = 4,
                BranchId = 1,
                ManagerId = 4,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 14,
                Name = "Tarek Mahmoud",
                Salary = 16000,
                HireDate = new DateOnly(2021, 12, 1),
                DepartmentId = 4,
                JobRoleId = 6,
                BranchId = 3,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 15,
                Name = "Salma Yasser",
                Salary = 10500,
                HireDate = new DateOnly(2023, 3, 12),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 2,
                ManagerId = 1,
                Status = EmploymentStatus.OnLeave
            },
            new Employee
            {
                Id = 16,
                Name = "Amr Khalil",
                Salary = 13000,
                HireDate = new DateOnly(2022, 8, 14),
                DepartmentId = 1,
                JobRoleId = 3,
                BranchId = 1,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 17,
                Name = "Reem Sameh",
                Salary = 11500,
                HireDate = new DateOnly(2023, 12, 3),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 3,
                ManagerId = 1,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 18,
                Name = "Mahmoud Essam",
                Salary = 15000,
                HireDate = new DateOnly(2022, 4, 22),
                DepartmentId = 4,
                JobRoleId = 2,
                BranchId = 2,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 19,
                Name = "Nada Ibrahim",
                Salary = 9000,
                HireDate = new DateOnly(2024, 2, 17),
                DepartmentId = 5,
                JobRoleId = 5,
                BranchId = 3,
                ManagerId = 4,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 20,
                Name = "Omar Khaled",
                Salary = 13500,
                HireDate = new DateOnly(2023, 7, 9),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 1,
                ManagerId = 1,
                Status = EmploymentStatus.Resigned
            },
            new Employee
            {
                Id = 21,
                Name = "Aya Samir",
                Salary = 10000,
                HireDate = new DateOnly(2024, 4, 10),
                DepartmentId = 2,
                JobRoleId = 4,
                BranchId = 2,
                ManagerId = 4,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 22,
                Name = "Khaled Adel",
                Salary = 14500,
                HireDate = new DateOnly(2022, 11, 28),
                DepartmentId = 4,
                JobRoleId = 6,
                BranchId = 1,
                ManagerId = 2,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 23,
                Name = "Farah Hany",
                Salary = 12000,
                HireDate = new DateOnly(2023, 5, 19),
                DepartmentId = 3,
                JobRoleId = 1,
                BranchId = 3,
                ManagerId = 1,
                Status = EmploymentStatus.Active
            },
            new Employee
            {
                Id = 24,
                Name = "Seif Mohamed",
                Salary = 9500,
                HireDate = new DateOnly(2024, 6, 25),
                DepartmentId = 1,
                JobRoleId = 3,
                BranchId = 2,
                ManagerId = 2,
                Status = EmploymentStatus.OnLeave
            },
            new Employee
            {
                Id = 25,
                Name = "Huda Hassan",
                Salary = 11000,
                HireDate = new DateOnly(2023, 10, 7),
                DepartmentId = 5,
                JobRoleId = 5,
                BranchId = 3,
                ManagerId = 4,
                Status = EmploymentStatus.Active
            }
        ];
        public static IReadOnlyList<EmployeeSkill> EmployeeSkills { get; } =
[
    new EmployeeSkill { EmployeeId = 1, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 1, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 1, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 1, SkillId = 4 },

    new EmployeeSkill { EmployeeId = 2, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 2, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 2, SkillId = 6 },
    new EmployeeSkill { EmployeeId = 2, SkillId = 7 },

    new EmployeeSkill { EmployeeId = 3, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 3, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 3, SkillId = 3 },

    new EmployeeSkill { EmployeeId = 4, SkillId = 7 },
    new EmployeeSkill { EmployeeId = 4, SkillId = 9 },

    new EmployeeSkill { EmployeeId = 5, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 5, SkillId = 7 },

    new EmployeeSkill { EmployeeId = 6, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 6, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 6, SkillId = 4 },
    new EmployeeSkill { EmployeeId = 6, SkillId = 10 },

    new EmployeeSkill { EmployeeId = 7, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 7, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 7, SkillId = 6 },

    new EmployeeSkill { EmployeeId = 8, SkillId = 4 },
    new EmployeeSkill { EmployeeId = 8, SkillId = 5 },
    new EmployeeSkill { EmployeeId = 8, SkillId = 9 },
    new EmployeeSkill { EmployeeId = 8, SkillId = 10 },

    new EmployeeSkill { EmployeeId = 9, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 9, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 9, SkillId = 6 },
    new EmployeeSkill { EmployeeId = 9, SkillId = 8 },

    new EmployeeSkill { EmployeeId = 10, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 10, SkillId = 7 },

    new EmployeeSkill { EmployeeId = 11, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 11, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 11, SkillId = 4 },

    new EmployeeSkill { EmployeeId = 12, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 12, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 12, SkillId = 6 },
    new EmployeeSkill { EmployeeId = 12, SkillId = 8 },

    new EmployeeSkill { EmployeeId = 13, SkillId = 7 },
    new EmployeeSkill { EmployeeId = 13, SkillId = 9 },

    new EmployeeSkill { EmployeeId = 14, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 14, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 14, SkillId = 6 },
    new EmployeeSkill { EmployeeId = 14, SkillId = 7 },
    new EmployeeSkill { EmployeeId = 14, SkillId = 8 },

    new EmployeeSkill { EmployeeId = 15, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 15, SkillId = 4 },
    new EmployeeSkill { EmployeeId = 15, SkillId = 10 },

    new EmployeeSkill { EmployeeId = 16, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 16, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 16, SkillId = 3 },

    new EmployeeSkill { EmployeeId = 17, SkillId = 4 },
    new EmployeeSkill { EmployeeId = 17, SkillId = 5 },
    new EmployeeSkill { EmployeeId = 17, SkillId = 9 },
    new EmployeeSkill { EmployeeId = 17, SkillId = 10 },

    new EmployeeSkill { EmployeeId = 18, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 18, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 18, SkillId = 6 },

    new EmployeeSkill { EmployeeId = 19, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 19, SkillId = 7 },

    new EmployeeSkill { EmployeeId = 20, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 20, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 20, SkillId = 4 },

    new EmployeeSkill { EmployeeId = 21, SkillId = 7 },
    new EmployeeSkill { EmployeeId = 21, SkillId = 9 },

    new EmployeeSkill { EmployeeId = 22, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 22, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 22, SkillId = 6 },
    new EmployeeSkill { EmployeeId = 22, SkillId = 8 },

    new EmployeeSkill { EmployeeId = 23, SkillId = 3 },
    new EmployeeSkill { EmployeeId = 23, SkillId = 4 },
    new EmployeeSkill { EmployeeId = 23, SkillId = 5 },
    new EmployeeSkill { EmployeeId = 23, SkillId = 10 },

    new EmployeeSkill { EmployeeId = 24, SkillId = 1 },
    new EmployeeSkill { EmployeeId = 24, SkillId = 3 },

    new EmployeeSkill { EmployeeId = 25, SkillId = 2 },
    new EmployeeSkill { EmployeeId = 25, SkillId = 7 }
];

        public static IReadOnlyList<EmployeeStatusHistory> EmployeeStatusHistories { get; } =
[
    new EmployeeStatusHistory
    {
        EmployeeId = 5,
        Status = EmploymentStatus.Active,
        ChangedAt = new DateOnly(2023, 9, 1)
    },
    new EmployeeStatusHistory
    {
        EmployeeId = 5,
        Status = EmploymentStatus.OnLeave,
        ChangedAt = new DateOnly(2026, 1, 10)
    },

    new EmployeeStatusHistory
    {
        EmployeeId = 15,
        Status = EmploymentStatus.Active,
        ChangedAt = new DateOnly(2023, 3, 12)
    },
    new EmployeeStatusHistory
    {
        EmployeeId = 15,
        Status = EmploymentStatus.OnLeave,
        ChangedAt = new DateOnly(2026, 2, 5)
    },

    new EmployeeStatusHistory
    {
        EmployeeId = 20,
        Status = EmploymentStatus.Active,
        ChangedAt = new DateOnly(2023, 7, 9)
    },
    new EmployeeStatusHistory
    {
        EmployeeId = 20,
        Status = EmploymentStatus.Resigned,
        ChangedAt = new DateOnly(2026, 4, 15)
    }
];


        public static IReadOnlyList<Employee> OrphanEmployees { get; } =
[
    new Employee
    {
        Id = 100,
        Name = "Orphan Employee",
        Salary = 10000,
        HireDate = new DateOnly(2024, 1, 1),
        DepartmentId = 999,
        JobRoleId = 1,
        BranchId = 1,
        ManagerId = null,
        Status = EmploymentStatus.Active
    }
];
    }
}