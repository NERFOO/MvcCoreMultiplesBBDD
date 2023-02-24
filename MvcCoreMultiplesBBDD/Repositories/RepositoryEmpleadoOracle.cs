using Microsoft.EntityFrameworkCore;
using MvcCoreMultiplesBBDD.Data;
using MvcCoreMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region
/*
CREATE OR REPLACE PROCEDURE SP_ALL_EMPLEADO
(P_CURSOR_EMPLEADOS OUT SYS_REFCURSOR)
AS
BEGIN
  OPEN P_CURSOR_EMPLEADOS FOR
  SELECT * FROM EMP;
END;


CREATE OR REPLACE PROCEDURE SP_DETAILS_EMPLEADO
(P_CURSOR_EMPLEADOS OUT SYS_REFCURSOR, P_IDEMPLEADO EMP.EMP_NO%TYPE)
AS
BEGIN
  OPEN P_CURSOR_EMPLEADOS FOR
  SELECT * FROM EMP
  WHERE EMP_NO = P_IDEMPLEADO;
END;


CREATE OR REPLACE PROCEDURE SP_DELETE_EMPLEADO
(P_IDEMPLEADO EMP.EMP_NO%TYPE)
AS
BEGIN
  DELETE FROM EMP WHERE EMP_NO = P_IDEMPLEADO;
  COMMIT;
END;


CREATE OR REPLACE PROCEDURE SP_UPDATE_EMPLEADO
(P_IDEMPLEADO EMP.EMP_NO%TYPE, P_SALARIO EMP.SALARIO%TYPE, P_OFICIO EMP.OFICIO%TYPE)
AS
BEGIN
  UPDATE EMP SET SALARIO = P_SALARIO, OFICIO = P_OFICIO
  WHERE EMP_NO = P_IDEMPLEADO;
  COMMIT;
END;
 
*/
#endregion

namespace MvcCoreMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadoOracle : IRepositoryEmpleado
    {
        private EmpleadoContext context;

        public RepositoryEmpleadoOracle(EmpleadoContext context)
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados()
        {
            string sql = "BEGIN SP_ALL_EMPLEADO(:P_CURSOR_EMPLEADOS); END;";

            OracleParameter paramCursor = new OracleParameter("P_CURSOR_EMPLEADOS", null);
            paramCursor.Direction = ParameterDirection.Output;
            paramCursor.OracleDbType = OracleDbType.RefCursor;

            var consulta = this.context.Empleados.FromSqlRaw(sql, paramCursor);

            List<Empleado> empleados = consulta.ToList();

            return empleados;
        }

        public Empleado FindEmpelado(int empleadoNum)
        {
            string sql = "BEGIN SP_DETAILS_EMPLEADO(:P_CURSOR_EMPLEADOS, :P_IDEMPLEADO); END;";

            OracleParameter paramCursor = new OracleParameter("P_CURSOR_EMPLEADOS", null);
            paramCursor.Direction = ParameterDirection.Output;
            paramCursor.OracleDbType = OracleDbType.RefCursor;

            OracleParameter paramEmpleadoNum = new OracleParameter("P_IDEMPLEADO", empleadoNum);

            var consulta = this.context.Empleados.FromSqlRaw(sql, paramCursor, paramEmpleadoNum);

            Empleado empleado = consulta.AsEnumerable().FirstOrDefault();

            return empleado;
        }

        public async Task DeleteEmpleado(int empleadoNum)
        {
            //EN ORACLE LOS PARAMETROS SE DENOMINAN MEDIANTE :PARAMETRO
            string sql = "BEGIN SP_DELETE_EMPLEADO(:P_IDEMPLEADO); END;";

            OracleParameter paramEmpleadoNum = new OracleParameter("P_IDEMPLEADO", empleadoNum);
            await this.context.Database.ExecuteSqlRawAsync(sql, paramEmpleadoNum);
        }

        public async Task UpdateEmpleado(int empleadoNum, int salario, string oficio)
        {
            string sql = "BEGIN SP_UPDATE_EMPLEADO(:P_IDEMPLEADO, :P_SALARIO, :P_OFICIO); END;";

            OracleParameter paramEmpleadoNum = new OracleParameter("P_IDEMPLEADO", empleadoNum);
            OracleParameter paramSalario = new OracleParameter("P_SALARIO", salario);
            OracleParameter paramOficio = new OracleParameter("P_OFICIO", oficio);

            await this.context.Database.ExecuteSqlRawAsync(sql, paramEmpleadoNum, paramSalario, paramOficio);
        }
    }
}
