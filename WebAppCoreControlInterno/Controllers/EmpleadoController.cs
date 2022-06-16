using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using WebAppCoreControlInterno.Models.ViewModels;
using Newtonsoft.Json;
using WebAppCoreControlInterno.Impinj;

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
        [Route("IndexEmpleado")]
        public async Task<IActionResult> IndexEmpleado(string nombre, string rut, string apellido)
        {
            ViewBag.Nombre = nombre;
            ViewBag.Rut = rut;
            ViewBag.Apellido = apellido;

            var empleados = from m in _context.Empleados.Include(b => b.FkIdCargoNavigation) select m;
            if (!String.IsNullOrEmpty(nombre))
            {
                empleados = empleados.Where(e => e.Nombre1.Contains(nombre) || e.Nombre2.Contains(nombre));
            }

            if (!String.IsNullOrEmpty(rut))
            {
                empleados = empleados.Where(e => e.Rut.Contains(rut));
            }

            if (!String.IsNullOrEmpty(apellido))
            {
                empleados = empleados.Where(e => e.Apellido1.Contains(apellido) || e.Apellido2.Contains(apellido));
            }

            return View(await empleados.ToListAsync());
        }



        ////Para obtener los datos de la Tabla Empleado
        //[Route("IndexEmpleadoFiltro")]
        //public async Task<IActionResult> IndexEmpleado(string nombre)
        //{
        //    var empleados = _context.Empleados.Where( e => e.Nombre1.Contains(nombre));
        //    //var empleados = _context.Empleados.Include(b => b.FkIdCargoNavigation);
        //    return View(await empleados.ToListAsync());
        //}



        //Para poblar el select Cargo
        public IActionResult CrearEmpleado()
        {
            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1");
            return View();
        }

        //Para crear empleados - por POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEmpleado(EmpleadoViewModel model)
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
                    return RedirectToAction(nameof(IndexEmpleado));
                }
            }
            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1", model.FkIdCargo);
            return View(model);
        }


        public IActionResult CrearEmpleadoConEpc()
        {

            //Reader r = new();
            List<string> listaEpc = new Reader().CrearLectura().Result;

            List<Tag> listaTags = new();
            foreach (var epc in listaEpc)
            {
                Tag tag = new();
                tag.Epc = epc;
                listaTags.Add(tag);
            }
            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1");
            ViewData["EPCs"] = new SelectList(listaTags, "Epc", "Epc");
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(_context.Cargos));
            return View();
        }


        //Para mostrar el Empleado a editar.
        public IActionResult EditarEmpleado(int Id)
        {
            EmpleadoViewModel model = new EmpleadoViewModel();

            var tEmpleado = _context.Empleados.Find(Id);

            model.IdEmpleado = tEmpleado.IdEmpleado;
            model.Rut = tEmpleado.Rut;
            model.Nombre1 = tEmpleado.Nombre1;
            model.Nombre2 = tEmpleado.Nombre2;
            model.Apellido1 = tEmpleado.Apellido1;
            model.Apellido2 = tEmpleado.Apellido2;
            model.Epc = tEmpleado.Epc;
            model.Fotografia = tEmpleado.Fotografia;
            model.FkIdCargo = tEmpleado.FkIdCargo;

            //System.Diagnostics.Debug.WriteLine( JsonConvert.SerializeObject( model ));

            ViewData["Cargos"] = new SelectList(_context.Cargos, "IdCargo", "Cargo1", model.FkIdCargo );

            return View(model);
        }


        //[Bind(include: "IdCargo, Cargo1")]
        //Para editar Cargo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarEmpleado( EmpleadoViewModel model )
        {
            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(model));

                var tEmpleado = _context.Empleados.Find(model.IdEmpleado);

                tEmpleado.Rut = model.Rut;
                tEmpleado.Nombre1 = model.Nombre1;
                tEmpleado.Nombre2 = model.Nombre2;
                tEmpleado.Apellido1 = model.Apellido1;
                tEmpleado.Apellido2 = model.Apellido2;
                tEmpleado.Epc = model.Epc;
                tEmpleado.Fotografia = model.Fotografia;
                tEmpleado.FkIdCargo = model.FkIdCargo;
                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                //System.Diagnostics.Debug.WriteLine("Si es Valido");

                _context.Entry(tEmpleado).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexEmpleado));
            }
            //System.Diagnostics.Debug.WriteLine("No es Valido");
            return View(model);
        }



        //Para confirmar eliminacion de Empleado.
        public IActionResult EliminarEmpleado(int Id)
        {
            EmpleadoViewModel model = new();

            var tEmpleado = _context.Empleados.Find(Id);

            model.IdEmpleado = tEmpleado.IdEmpleado;
            model.Rut = tEmpleado.Rut;
            model.Nombre1 = tEmpleado.Nombre1;
            model.Nombre2 = tEmpleado.Nombre2;
            model.Apellido1 = tEmpleado.Apellido1;
            model.Apellido2 = tEmpleado.Apellido2;
            model.Epc = tEmpleado.Epc;
            model.Fotografia = tEmpleado.Fotografia;
            model.FkIdCargo = tEmpleado.FkIdCargo;

            return View(model);
        }


        //Para eliminar Empleado.
        [HttpPost, ActionName("EliminarEmpleado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
