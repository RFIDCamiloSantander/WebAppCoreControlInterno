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
    public class SectorController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;


        //Se asigna el conexto(BD) a la variable
        public SectorController(ControlInternoContext context)
        {
            _context = context;
        }



        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexSector(string nombre, string sucursal)
        {
            ViewBag.Nombre = nombre;

            ViewBag.Sucursal = sucursal;

            var sectores = from m in _context.Sectors.Include(b => b.FkIdSucursalNavigation) select m;

            if (!String.IsNullOrEmpty(nombre))
            {
                sectores = sectores.Where( m => m.Nombre.Contains(nombre));
            }

            if (!String.IsNullOrEmpty(sucursal))
            {
                sectores = sectores.Where(m => m.FkIdSucursalNavigation.Nombre.Contains(sucursal));
            }

            return View( await sectores.ToListAsync());
        }



        //Para mostrar Vista para Crear.
        public IActionResult CrearSector()
        {
            ViewBag.Sucursals = new SelectList(_context.Sucursals, "IdSucursal", "Nombre");
            //System.Diagnostics.Debug.WriteLine("El viewBag" + ViewBag.Cargos );
            return View();
        }



        //Para crear Sector
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSector(SectorViewModel model)
        {
            //System.Diagnostics.Debug.WriteLine("el viewbag");

            if (ModelState.IsValid)
            {
                Sector sector = new()
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    FkIdSucursal = model.FkIdSucursal,
                };

                _context.Add(sector);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexSector));
            }
            ViewData["Sucursals"] = new SelectList(_context.Sucursals, "IdSucursal", "Nombre", model.FkIdSucursal);
            return View(model);
        }



        //Para mostrar el Cargo a editar.
        public IActionResult EditarSector(int Id)
        {
            var sector = _context.Sectors.Find(Id);

            SectorViewModel model = new()
            {
                IdSector = sector.IdSector,
                Nombre = sector.Nombre,
                Descripcion = sector.Descripcion,
                FkIdSucursal = sector.FkIdSucursal,
            };

            ViewData["Sucursals"] = new SelectList(_context.Sucursals, "IdSucursal", "Nombre", model.FkIdSucursal);

            return View(model);
        }



        //Para editar Sector.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarSector([Bind(include: "IdSector, Nombre, Descripcion, FkIdSucursal")] SectorViewModel model)
        {

            if (ModelState.IsValid)
            {
                var sector = _context.Sectors.Find(model.IdSector);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                sector.Nombre = model.Nombre;
                sector.Descripcion = model.Descripcion;
                sector.FkIdSucursal = model.FkIdSucursal;

                _context.Entry(sector).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexSector));
            }
            return View(model);
        }



        //Para confirmar eliminacion de Sector.
        public IActionResult EliminarSector(int Id)
        {
            ViewBag.Errors = false;

            var subSector = _context.SubSectors.Where(m => m.FkIdSector.Equals(Id));

            if (subSector.Any())
            {
                ViewBag.Errors = true;
            }

            var sector = _context.Sectors.Find(Id);

            SectorViewModel model = new()
            {
                IdSector = sector.IdSector,
                Nombre = sector.Nombre,
                Descripcion = sector.Descripcion,
                FkIdSucursal = sector.FkIdSucursal,
            };

            return View(model);
        }



        //Para eliminar Cargo.
        [HttpPost, ActionName("EliminarSector")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var sector = await _context.Sectors.FindAsync(id);
            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSector));
        }
    }
}