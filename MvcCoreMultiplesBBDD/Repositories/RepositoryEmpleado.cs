using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using System.Data;
using System.Data.Common;

#region
/*

CREATE PROCEDURE SP_TODOS_EMPELADOS
AS
	SELECT * FROM EMP


CREATE PROCEDURE SP_FIND_EMPLEADO
(@EMPLEADONUM INT)
AS
	SELECT * FROM EMP WHERE EMP_NO = @EMPLEADONUM
*/
#endregion

namespace MvcCoreMultiplesBBDD.Repositories
{
	public class RepositoryEmpleado : IRepositoryEmpleado
	{
		private EmpleadoContext context;

		public RepositoryEmpleado(EmpleadoContext context)
		{
			this.context = context;
		}

		public List<Empleado> GetEmpleados()
		{
			string sql = "SP_TODOS_EMPELADOS";

			var consulta = this.context.Empleados.FromSqlRaw(sql);

			List<Empleado> empleados = consulta.ToList();

			return empleados;
        }

		public Empleado FindEmpelado(int empleadoNum)
		{
			var consulta = from datos in this.context.Empleados
							where datos.EmpleadoNum == empleadoNum
							select datos;
			return consulta.ToList().FirstOrDefault();
		}

		public async Task DeleteEmpleado(int empleadoNum)
		{
			Empleado empleado = this.FindEmpelado(empleadoNum);
			this.context.Empleados.Remove(empleado);

			await this.context.SaveChangesAsync();
		}

		public async Task UpdateEmpleado(int empleadoNum, int salario, string oficio)
		{
			Empleado empleado = this.FindEmpelado(empleadoNum);
			empleado.Salario = salario;
			empleado.Oficio = oficio;

			await this.context.SaveChangesAsync();
		}
	}
}
