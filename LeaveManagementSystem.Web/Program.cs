using LeaveManagementSystem.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Start - Added Services

// Services has been moved here
DataServicesRegistration.AddDataServices(builder.Services, builder.Configuration);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
//                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

ApplicationServicesRegistration.AddApplicationServices(builder.Services);

builder.Host.UseSerilog((ctx, config) =>
    config.WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration)
);

// Adding Policy to resource
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminSupervisorOnly", policy =>
    {
        policy.RequireRole(Roles.Administrator, Roles.Supervisor);
    });

    options.AddPolicy("SupervisorOnly", policy =>
    {
        policy.RequireRole(Roles.Supervisor);
    });
});

builder.Services.AddHttpContextAccessor();

// End - Added Services

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 8;
})
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
