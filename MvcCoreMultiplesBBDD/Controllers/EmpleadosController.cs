using Microsoft.AspNetCore.Mvc;
using MvcCoreMultiplesBBDD.Models;
using MvcCoreMultiplesBBDD.Repositories;

namespace MvcCoreMultiplesBBDD.Controllers
{
	public class EmpleadosController : Controller
	{
		private IRepositoryEmpleado irepo;

		public EmpleadosController(IRepositoryEmpleado irepo)
		{
			this.irepo = irepo;
		}

		public IActionResult Index()
		{
			List<Empleado> empleados = this.irepo.GetEmpleados();
			return View(empleados);
		}

		public IActionResult Details(int empleadoNum)
		{
			Empleado empleado = this.irepo.FindEmpelado(empleadoNum);
			return View(empleado);
		}

		public async Task<IActionResult> Delete(int empleadoNum)
		{
			await this.irepo.DeleteEmpleado(empleadoNum);
			return RedirectToAction("Index");
        }

        public IActionResult UpdateEmpleado(int empleadoNum)
        {
            Empleado empleado = this.irepo.FindEmpelado(empleadoNum);
            return View(empleado);
        }

		[HttpPost]
        public async Task<IActionResult> UpdateEmpleado(Empleado empleado)
        {
			await this.irepo.UpdateEmpleado(empleado.EmpleadoNum, empleado.Salario, empleado.Oficio);
			return RedirectToAction("Index");
        }

    }
}
