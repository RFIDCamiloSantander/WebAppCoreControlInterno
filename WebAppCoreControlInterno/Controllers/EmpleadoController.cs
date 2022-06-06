using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using WebAppCoreControlInterno.Models.ViewModels;

namespace WebAppCoreControlInterno.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ControlInternoContext _context;

        //Se obtiene el context del controlador
        public EmpleadoController(ControlInternoContext context)
        {
            _context = context;
        }


        //Para obtener los datos de la Tabla Empleado
        public async Task<IActionResult> Index()
        {
            var empleados = _context.Empleados.Include(b => b.FkIdCargoNavigation);
            return View(await empleados.ToListAsync());
        }


        //Chequeando que hace
        public IActionResult Create()
        {
            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1");
            return View();
        }


        //Para crear empleados - por POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Contrasena == model.Contrasena2)
                {
                    var empleado = new Empleado()
                    {
                        Rut = model.Rut,
                        Nombre1 = model.Nombre1,
                        Nombre2 = model.Nombre2,
                        Apellido1 = model.Apellido1,
                        Apellido2 = model.Apellido2,
                        Epc = model.Epc,
                        Fotografia = model.Fotografia,
                        Contrasena = model.Contrasena,
                        FkIdCargo = model.FkIdCargo,
                        Custom1 = model.Custom1,
                        Custom2 = model.Custom2,
                        Custom3 = model.Custom3,
                    };
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["Cargos"] = new SelectList(_context.Empleados, "IdCargo", "Cargo1", model.FkIdCargo);
            return View(model);
        }
    }
}
