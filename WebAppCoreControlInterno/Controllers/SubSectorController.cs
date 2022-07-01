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
    public class SubSectorController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;


        //Se asigna el conexto(BD) a la variable
        public SubSectorController(ControlInternoContext context)
        {
            _context = context;
        }



        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexSubSector(string nombre, string epc, string sucursal)
        {
            ViewBag.Nombre = nombre;
            ViewBag.Epc = epc;
            ViewBag.Sucursal = sucursal;

            var SubSectors = from m in _context.SubSectors.Include(m => m.FkIdSectorNavigation) select m;

            if (!String.IsNullOrEmpty(nombre))
            {
                SubSectors = SubSectors.Where(m => m.Nombre.Contains(nombre));
            }

            if (!String.IsNullOrEmpty(epc))
            {
                SubSectors = SubSectors.Where(m => m.Epc.Contains(epc));
            }

            if (!String.IsNullOrEmpty(sucursal))
            {
                SubSectors = SubSectors.Where(m => m.FkIdSectorNavigation.Nombre.Contains(sucursal));
            }

            return View(await SubSectors.ToListAsync());
        }



        //Para mostrar Vista para Crear.
        public IActionResult CrearSubSector()
        {
            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre");
            return View();
        }



        //Para crear Sector
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSubSector(SubSectorViewModel model)
        {
            var tSubSector = _context.SubSectors.Where(m => m.Nombre.Equals(model.Nombre));

            if (tSubSector.Any())
            {
                ViewBag.ErrorMessage = "Ya existe un SubSector con este nombre";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                SubSector subSector = new()
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    Epc = model.Epc,
                    FkIdSector = model.FkIdSector,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };

                _context.Add(subSector);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexSubSector));
            }

            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre", model.FkIdSector);
            return View(model);
        }



        //Para mostrar el Cargo a editar.
        public IActionResult EditarSubSector(int Id)
        {
            SubSector subSector = _context.SubSectors.Find(Id);

            SubSectorViewModel model = new()
            {
                IdSubSector = subSector.IdSubSector,
                Nombre = subSector.Nombre,
                Descripcion = subSector.Descripcion,
                Epc = subSector.Epc,
                FkIdSector = subSector.FkIdSector,
                Custom1 = subSector.Custom1,
                Custom2 = subSector.Custom2,
                Custom3 = subSector.Custom3,
            };

            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre", model.FkIdSector);
            return View(model);

        }



        //Para editar SubSector.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarSubSector([Bind(include: "IdSubSector, Nombre, Descripcion, Epc, FkIdSector")] SubSectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subSector = _context.SubSectors.Find(model.IdSubSector);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                subSector.Nombre = model.Nombre;
                subSector.Descripcion = model.Descripcion;
                subSector.Epc = model.Epc;
                subSector.FkIdSector = model.FkIdSector;

                _context.Entry(subSector).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexSubSector));
            }

            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre", model.FkIdSector);
            return View(model);

        }



        //Para confirmar eliminacion de SubSector.
        public IActionResult EliminarSubSector(int Id)
        {
            ViewBag.Errors = false;
            var subSector = _context.SubSectors.Find(Id);
            var elemento = _context.Elementos.Where(m => m.FkIdSubSector.Equals(Id));
            var movimiento = _context.Movimientos.Where(m => m.FkIdSubSector.Equals(Id));

            if (elemento.Any() || movimiento.Any())
            {
                ViewBag.Errors = true;
            }

            SubSectorViewModel model = new()
            {
                IdSubSector = subSector.IdSubSector,
                Nombre = subSector.Nombre,
                Descripcion = subSector.Descripcion,
                Epc = subSector.Epc,
                FkIdSector= subSector.FkIdSector,
            };

            return View(model);
        }



        //Para eliminar Cargo.
        [HttpPost, ActionName("EliminarSubSector")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var subSector = await _context.SubSectors.FindAsync(id);
            _context.SubSectors.Remove(subSector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSubSector));
        }
    }
}