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
            return View(await _context.Empleados.ToListAsync());
        }

        //Chequeando que hace
        public IActionResult Create()
        {
            ViewData["Empleados"] = new SelectList( _context.Empleados, "");
            return View();
        }

        //Para crear empleados
        [HttpPost]
        public IActionResult Create(int a)
        {
            ViewData["Empleados"] = new SelectList(_context.Empleados, "");
            return View();
        }
    }
}
