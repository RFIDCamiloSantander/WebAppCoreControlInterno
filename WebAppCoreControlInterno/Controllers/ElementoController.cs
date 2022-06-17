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
    public class ElementoController : Controller
    {

        //Se crea la variable para el conexto (BD).
        private readonly ControlInternoContext _context;


        //Se asigna el conexto (BD) a la variable.
        public ElementoController(ControlInternoContext context)
        {
            _context = context;
        }



        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexElemento()
        {
            ViewBag.Bases = new SelectList(_context.Bases, "IdBase", "Nombre");
            ViewBag.Estados = new SelectList(_context.Estados, "IdEstado", "Estado1");
            ViewBag.Sucursals = new SelectList(_context.Sucursals, "IdSucursal", "Nombre");
            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre");
            ViewBag.SubSectors = new SelectList(_context.SubSectors, "IdSubSector", "Nombre");
            ViewBag.Empleados = new SelectList(_context.Empleados, "IdEmpleado", "Nombre1");
            return View(await _context.Elementos.ToListAsync());
        }



        //Para mostrar Vista para Crear Elemento.
        public IActionResult CrearElemento()
        {
            return View();
        }



        //Para crear Elemento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearElemento(ElementoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Elemento elemento = new()
                {
                    Epc = model.Epc,
                    FechaIngreso = model.FechaIngreso,
                    FechaUltimaLectura = model.FechaUltimaLectura,
                    Dimension = model.Dimension,
                    PesoKg = model.PesoKg,
                    NroSerie = model.NroSerie,
                    NroParte = model.NroParte,
                    InformacionExtra = model.InformacionExtra,
                    Fotografia = model.Fotografia,
                    FkIdElementoBase = model.FkIdElementoBase,
                    FkIdEstado = model.FkIdEstado,
                    FkIdSucursal = model.FkIdSucursal,
                    FkIdSector = model.FkIdSector,
                    FkIdSubSector = model.FkIdSubSector,
                    FkIdEmpleadoEncargado = model.FkIdEmpleadoEncargado,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };

                _context.Add(elemento);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexElemento));
            }
            return View(model);
        }



        //Para mostrar el Elemento a editar.
        public IActionResult EditarElemento(int Id)
        {
            Elemento elemento = _context.Elementos.Find(Id);

            ElementoViewModel model = new()
            {
                Epc = elemento.Epc,
                FechaIngreso = elemento.FechaIngreso,
                FechaUltimaLectura = elemento.FechaUltimaLectura,
                Dimension = elemento.Dimension,
                PesoKg = elemento.PesoKg,
                NroSerie = elemento.NroSerie,
                NroParte = elemento.NroParte,
                InformacionExtra = elemento.InformacionExtra,
                Fotografia = elemento.Fotografia,
                FkIdElementoBase = elemento.FkIdElementoBase,
                FkIdEstado = elemento.FkIdEstado,
                FkIdSucursal = elemento.FkIdSucursal,
                FkIdSector = elemento.FkIdSector,
                FkIdSubSector = elemento.FkIdSubSector,
                FkIdEmpleadoEncargado = elemento.FkIdEmpleadoEncargado,
                Custom1 = elemento.Custom1,
                Custom2 = elemento.Custom2,
                Custom3 = elemento.Custom3,
            };

            return View(model);
        }



        //Para editar SubSector.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarElemento([Bind(include: "IdElemento, Epc, FechaIngreso, FechaUltimaLectura, Dimension, PesoKg, NroSerie, NroParte, InformacionExtra, Fotografia, FkIdElementoBase, FkIdEstado, FkIdSucursal, FkIdSector, FkIdEmpleadoEncargado, Custom1, Custom2, Custom3")] ElementoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var elemento = _context.Elementos.Find(model.IdElemento);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                elemento.Epc = model.Epc;
                elemento.FechaIngreso = model.FechaIngreso;
                elemento.FechaUltimaLectura = model.FechaUltimaLectura;
                elemento.Dimension = model.Dimension;
                elemento.PesoKg = model.PesoKg;
                elemento.NroSerie = model.NroSerie;
                elemento.NroParte = model.NroParte;
                elemento.InformacionExtra = model.InformacionExtra;
                elemento.Fotografia = model.Fotografia;
                elemento.FkIdElementoBase = model.FkIdElementoBase;
                elemento.FkIdEstado= model.FkIdEstado;
                elemento.FkIdSucursal = model.FkIdSucursal;
                elemento.FkIdSector = model.FkIdSector;
                elemento.FkIdSubSector = model.FkIdSubSector;
                elemento.FkIdEmpleadoEncargado = model.FkIdEmpleadoEncargado;
                elemento.Custom1 = model.Custom1;
                elemento.Custom2 = model.Custom2;
                elemento.Custom3 = model.Custom3;

                _context.Entry(elemento).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexElemento));
            }
            return View(model);
        }



        //Para confirmar eliminacion de SubSector.
        public IActionResult EliminarElemento(int Id)
        {
            var elemento = _context.Elementos.Find(Id);

            ElementoViewModel model = new()
            {
                IdElemento = elemento.IdElemento,
                Epc = elemento.Epc,
                FechaIngreso = elemento.FechaIngreso,
                FechaUltimaLectura = elemento.FechaUltimaLectura,
                Dimension = elemento.Dimension,
                PesoKg = elemento.PesoKg,
                NroSerie = elemento.NroSerie,
                NroParte = elemento.NroParte,
                InformacionExtra = elemento.InformacionExtra,
                Fotografia = elemento.Fotografia,
                FkIdElementoBase = elemento.FkIdElementoBase,
                FkIdEstado = elemento.FkIdEstado,
                FkIdSucursal = elemento.FkIdSucursal,
                FkIdSector = elemento.FkIdSector,
                FkIdSubSector = elemento.FkIdSubSector,
                FkIdEmpleadoEncargado = elemento.FkIdEmpleadoEncargado,
                Custom1 = elemento.Custom1,
                Custom2 = elemento.Custom2,
                Custom3 = elemento.Custom3,
            };

            return View(model);
        }



        //Para eliminar Elemento.
        [HttpPost, ActionName("EliminarElemento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            _context.Elementos.Remove(elemento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexElemento));
        }
    }
}
