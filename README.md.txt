# Employee Directory Manager — Part 4

C# Fundamentals & LINQ training project for the GEO Solutions Junior Developer Training Program.

## Solution Structure

- EmployeeDirectory.Domain
  - Domain models and EmploymentStatus enum.
- EmployeeDirectory.Data
  - Seed data.
  - EF Core database-first models and DbContext.
- EmployeeDirectory.Exercises
  - LINQ and EF Core exercises from B.1 to B.11.
- EmployeeDirectory.Tests
  - xUnit tests for the exercises.
- EmployeeDirectory.Console
  - Optional console project.

## Technologies

- C#
- .NET 9
- LINQ
- Entity Framework Core 9
- SQL Server LocalDB
- xUnit

## How to Run

1. Open the solution in Visual Studio.
2. Build the solution.
3. Open Test Explorer.
4. Run all tests.
5. All exercises are implemented as public static methods and verified through xUnit tests.

## LINQ Exercises

The project covers:

- Filtering and projection
- Indexed Select
- DistinctBy
- Ordering and paging
- Chunk
- First / FirstOrDefault / Single / SingleOrDefault
- MaxBy / MinBy
- Inner Join
- Query syntax
- GroupJoin
- Left outer join
- Full outer join
- Self join
- Cross join
- Many-to-many relationships
- Union / Concat / Intersect / Except
- UnionBy / IntersectBy / ExceptBy
- Custom equality comparers
- GroupBy and aggregates
- Aggregate and running totals
- ToLookup / GroupBy / ToDictionary
- ROLLUP equivalent
- Row numbering and ranking
- Quartiles / NTILE
- Lag / Lead
- Top-N per group
- Quantifiers
- Recursive employee hierarchies
- Cycle detection
- Deferred and immediate execution
- Multiple enumeration
- LINQ performance measurement
- Extension methods
- WhereIf
- Lazy Pairwise
- Func vs Expression<Func>

## Entity Framework Core

B.11 demonstrates:

- Database-first scaffolding
- LINQ queries against DbContext
- ToQueryString()
- Include / ThenInclude
- DTO projection
- N+1 query pattern and its fix
- Server-side and client-side evaluation
- AsNoTracking
- Async database queries
- CancellationToken

## Design Decisions

- Exercises return typed results instead of writing directly to the console.
- Named record types are used instead of anonymous types across method boundaries.
- LINQ queries are kept readable and composable.
- Seed data is exposed through read-only collection interfaces.
- Database read operations use AsNoTracking where tracking is unnecessary.
- DTO projection is used when only selected columns are required.
- EF Core queries are kept server-side whenever possible.
- The project targets .NET 9 and uses EF Core 9.

## SQL Server Database

The EF Core database-first part uses:

```text
(localdb)\MSSQLLocalDB

Database:

EmployeeDirectoryDb

The database contains the tables required for the EF Core exercises and relationships between employees, departments, job roles, branches, skills, and status histories.

SQL vs LINQ

LINQ and SQL solve many of the same querying problems, but they work at different levels.

SQL is designed specifically for querying relational databases and provides direct control over database-side operations such as joins, grouping, filtering, aggregation, and ordering.

LINQ provides a strongly typed C# query syntax that can be used with in-memory collections and data providers such as Entity Framework Core. With EF Core, LINQ expressions can be translated into SQL and executed by the database.

For database queries, keeping filtering, ordering, projection, and aggregation before materialization generally allows EF Core to translate more work to SQL. Calling ToList() too early can move subsequent operations into application memory.

LINQ is useful because the same programming language can be used for both in-memory collections and database queries, while SQL remains the underlying language executed by a relational database.

Performance Notes

B.9.3 measures LINQ query and materialization times using a generated collection of 100,000 employees.

The measured values are machine-dependent and can vary between runs.

Test Status

The current test suite contains:

55 tests
55 passed
0 failed
Git Workflow

The project should be committed to a GitHub repository using a feature branch.

Recommended workflow:

Create the repository.
Create a feature branch for Part 4.
Commit the completed implementation.
Push the feature branch.
Open a Pull Request from the feature branch into main.
Merge the Pull Request after review.
Avoid direct development commits to main.