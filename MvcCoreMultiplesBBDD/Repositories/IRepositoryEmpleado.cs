using MvcCoreMultiplesBBDD.Models;

namespace MvcCoreMultiplesBBDD.Repositories
{
	public interface IRepositoryEmpleado
	{
		public List<Empleado> GetEmpleados();

		public Empleado FindEmpelado(int empleadoNum);
		public Task DeleteEmpleado(int empleadoNum);
		public Task UpdateEmpleado(int empleadoNum, int salario, string oficio);
	}
}
