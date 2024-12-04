using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LeaveManagementSystem.Web.Data;

namespace LeaveManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "45fbb2f4-87fa-4605-9c5f-912fa126fc44",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole 
                {
                    Id = "f6f3a032-cb87-4620-94f4-ed3ce3821f7a",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole 
                {
                    Id = "e62ab376-0761-4fb6-9a45-270084291888",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8f3fe8df-fa03-4fae-9752-fa102410c5b8",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true,
                    FirstName = "Ariel",
                    LastName = "Onayan",
                    DateOfBirth = new DateOnly(2000, 09, 10)
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e62ab376-0761-4fb6-9a45-270084291888",
                    UserId = "8f3fe8df-fa03-4fae-9752-fa102410c5b8"
                });

            // Ariel - Sample data migration - Adding Leave Type
            builder.Entity<LeaveType>().HasData(
                new LeaveType
                { 
                    Id = 51,
                    Name = "Holiday Leave",
                    NumberOfDays = 1,
                });
        }

        public DbSet<LeaveType> LeaveTypes { get; set; }

    }
}
