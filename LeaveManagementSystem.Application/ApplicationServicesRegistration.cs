using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LeaveManagementSystem.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Start - Added Services
            services.AddScoped<ILeaveTypeService, LeaveTypeService>(); // This was added for Service and Interface
            //services.AddTransient<IEmailSender, EmailSender>(); // This was added for Service and Interface
            services.AddScoped<ILeaveAllocationService, LeaveAllocationService>(); // This was added for Service and Interface
            services.AddScoped<ILeaveRequestsService, LeaveRequestsService>(); // This was added for Service and Interface
            services.AddScoped<IPeriodService, PeriodsService>(); // This was added for Service and Interface
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
