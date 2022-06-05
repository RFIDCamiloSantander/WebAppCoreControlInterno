using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

namespace WebAppCoreControlInterno.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ControlInternoContext _context;

        //Se obtiene el context del controlador
        public EmpleadoController( ControlInternoContext context )
        {
            _context = context;
        }

        //Para obtener los datos de la Tabla Empleado
        public async Task<IActionResult> Index()
        {
            var empleados = _context.Empleados.Include( b => b.FkIdCargoNavigation );
            return View( await empleados.ToListAsync() );
        }

        //Chequeando que hace
        public IActionResult Create()
        {
            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1");
            return View();
        }

        //Para crear empleados
        [HttpPost]
        public IActionResult Create(int a)
        {
            ViewData["Cargos"] = new SelectList(_context.Empleados, "");
            return View();
        }
    }
}
