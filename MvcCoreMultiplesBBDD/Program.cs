using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
//builder.Services.AddTransient<IRepositoryEmpleado, RepositoryEmpleado>();
//builder.Services.AddDbContext<EmpleadoContext>(options => options.UseSqlServer(connectionString));

string connectionString = builder.Configuration.GetConnectionString("OracleHospital");
builder.Services.AddTransient<IRepositoryEmpleado, RepositoryEmpleadoOracle>();
builder.Services.AddDbContext<EmpleadoContext>(options => options.UseOracle(connectionString));

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Empleados}/{action=Index}/{id?}");

app.Run();
