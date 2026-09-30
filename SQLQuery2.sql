CREATE TABLE Departments
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    ParentDepartmentId INT NULL
);

CREATE TABLE JobRoles
(
    Id INT PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    DepartmentId INT NOT NULL
);

CREATE TABLE Branches
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    City NVARCHAR(100) NOT NULL
);

CREATE TABLE Skills
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Employees
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    HireDate DATE NOT NULL,
    DepartmentId INT NOT NULL,
    JobRoleId INT NOT NULL,
    BranchId INT NOT NULL,
    ManagerId INT NULL,
    Status INT NOT NULL
);

CREATE TABLE EmployeeSkills
(
    EmployeeId INT NOT NULL,
    SkillId INT NOT NULL,
    PRIMARY KEY (EmployeeId, SkillId)
);

CREATE TABLE EmployeeStatusHistories
(
    EmployeeId INT NOT NULL,
    Status INT NOT NULL,
    ChangedAt DATE NOT NULL
);