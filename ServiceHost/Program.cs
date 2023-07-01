using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.Configuration;
using AccountManagement.Infrastructure.EFCore;
using Identity.Bugeto.Helpers;
using Microsoft.AspNetCore.Identity;
using NewsManagement.Infrastructure.Configuration;
using ServiceHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cs = builder.Configuration.GetConnectionString("sqlserver");

NewsManagementBootstrapper.Config(builder.Services,cs);
AccountManagementBootstrapper.Config(builder.Services, cs);

builder.Services.AddIdentity<User, Role>()
	.AddEntityFrameworkStores<AccountManagementDbContext>()
	.AddDefaultTokenProviders()
	.AddErrorDescriber<CustomIdentityError>();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
	options.LoginPath = "/account/login";
});

builder.Services.AddTransient<IFileUploader, FileUploader>();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminArea",
		builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

	options.AddPolicy("Administration",
		builder => builder.RequireRole(new List<string> { Roles.Administrator }));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});







app.Run();
