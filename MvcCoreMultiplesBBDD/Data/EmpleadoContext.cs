using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Data
{
	public class EmpleadoContext : DbContext
	{
		public EmpleadoContext(DbContextOptions<EmpleadoContext> options) :base(options) { }

		public DbSet<Empleado> Empleados { get; set; }
	}
}
