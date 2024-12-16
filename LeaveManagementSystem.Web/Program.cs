using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Services.LeaveAllocations;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using LeaveManagementSystem.Web.Services.Periods;
using LeaveManagementSystem.Web.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Start - Added Services
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>(); // This was added for Service and Interface
//builder.Services.AddTransient<IEmailSender, EmailSender>(); // This was added for Service and Interface
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>(); // This was added for Service and Interface
builder.Services.AddScoped<ILeaveRequestsService, LeaveRequestsService>(); // This was added for Service and Interface
builder.Services.AddScoped<IPeriodService, PeriodsService>(); // This was added for Service and Interface
builder.Services.AddScoped<IUserService, UserService>();

// Adding Policy to resource
builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminSupervisorOnly", policy => {
        policy.RequireRole(Roles.Administrator, Roles.Supervisor);
    });

    options.AddPolicy("SupervisorOnly", policy => {
        policy.RequireRole(Roles.Supervisor);
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // This was added for the AutoMapper

// End - Added Services

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
