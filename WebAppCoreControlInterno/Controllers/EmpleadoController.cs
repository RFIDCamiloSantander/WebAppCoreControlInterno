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


        //Para mostrar el Cargo a editar.
        public IActionResult EditarEmpleado(int Id)
        {
            EmpleadoViewModel model = new EmpleadoViewModel();

            var tEmpleado = _context.Empleados.Find(Id);

            model.Rut = tEmpleado.Rut;
            model.Nombre1 = tEmpleado.Nombre1;
            model.Nombre2 = tEmpleado.Nombre2;
            model.Apellido1 = tEmpleado.Apellido1;
            model.Apellido2 = tEmpleado.Apellido2;
            model.Epc = tEmpleado.Epc;
            model.Fotografia = tEmpleado.Fotografia;
            model.Fotografia = tEmpleado.FkIdCargoNavigation.Cargo1;

            return View(model);
        }


        //Para editar Cargo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([Bind(include: "IdCargo, Cargo1")] CargoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tCargo = _context.Cargos.Find(model.IdCargo);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                tCargo.Cargo1 = model.Cargo1;

                _context.Entry(tCargo).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
