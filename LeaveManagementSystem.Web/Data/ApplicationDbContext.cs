using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Data.Configuration;
using Microsoft.Build.Execution;
using System.Reflection;

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
            // This line of code will do all the apply configuration below so no need to apply configuration
            // for each configuration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //builder.ApplyConfiguration(new IdentityRoleConfiguration());
            //builder.ApplyConfiguration(new ApplicationUserConfiguration());
            //builder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            //builder.ApplyConfiguration(new LeaveRequestStatusConfiguration());

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

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        public DbSet<Period> Periods { get; set; }

        public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
