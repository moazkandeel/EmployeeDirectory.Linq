using System;
using System.Collections.Generic;
using EmployeeDirectory.Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Data;

public partial class EmployeeDirectoryDbContext : DbContext
{
    public EmployeeDirectoryDbContext()
    {
    }

    public EmployeeDirectoryDbContext(
        DbContextOptions<EmployeeDirectoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeStatusHistory> EmployeeStatusHistories { get; set; }

    public virtual DbSet<JobRole> JobRoles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Branches__3214EC07DDA3A9D1");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.City)
                .HasMaxLength(100);

            entity.Property(e => e.Name)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Departme__3214EC07F4FF332D");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.HasOne(d => d.ParentDepartment)
                .WithMany(p => p.InverseParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .HasConstraintName("FK_Departments_ParentDepartment");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Employee__3214EC07B2B825AA");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(100);

            entity.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Branch)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Branches");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.JobRole)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.JobRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_JobRoles");

            entity.HasOne(d => d.Manager)
                .WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Employees_Manager");

            entity.HasMany(d => d.Skills)
                .WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeSkill",
                    r => r.HasOne<Skill>()
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeSkills_Skills"),
                    l => l.HasOne<Employee>()
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeSkills_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "SkillId")
                            .HasName("PK__Employee__172A460963B51D91");

                        j.ToTable("EmployeeSkills");
                    });
        });

        modelBuilder.Entity<EmployeeStatusHistory>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeStatusHistories_Employees");
        });

        modelBuilder.Entity<JobRole>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__JobRoles__3214EC07DF3FE7CE");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Title)
                .HasMaxLength(100);

            entity.HasOne(d => d.Department)
                .WithMany(p => p.JobRoles)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobRoles_Departments");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Skills__3214EC079BAD5746");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}