using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebAPIUsingCore.Models
{
    public partial class OfficeDBContext : DbContext
    {
        public OfficeDBContext()
        {
        }

        public OfficeDBContext(DbContextOptions<OfficeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlConnectionString"));
                //optionsBuilder.UseSqlServer("Data Source=BAN-L170667\\LOCALHOST;Database=OfficeDB;Trusted_Connection=True;Connection Timeout=1000000;");
                //optionsBuilder.UseSqlServer("Data Source=localhost\\MSSQLSERVER01;Database=myDatabase;Trusted_Connection=True;Connection Timeout=1000000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.RowId)
                    .HasName("PK__Departme__FFEE743166E6D4C0");

                entity.ToTable("Department");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.RowId)
                    .HasName("PK__Employee__FFEE7431E5E57ECB");

                entity.ToTable("Employee");

                entity.Property(e => e.DateOfJoining).HasColumnType("date");

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
