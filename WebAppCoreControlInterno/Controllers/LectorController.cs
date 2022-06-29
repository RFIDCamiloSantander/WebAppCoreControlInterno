using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using WebAppCoreControlInterno.Models.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppCoreControlInterno.Controllers
{
    public class LectorController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;


        //Se asigna el conexto(BD) a la variable
        public LectorController(ControlInternoContext context)
        {
            _context = context;
        }



        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexLector(string mac, string ip, string nroSerie, string nroParte, string marca, string modelo, string sucursal)
        {
            //var lectores = await _context.Lectors.ToListAsync();

            //return View( lectores );

            var lectors = from m in _context.Lectors.Include(m => m.FkIdSucursalNavigation) select m;

            if (!String.IsNullOrEmpty(mac))
            {
                lectors = lectors.Where(m => m.Mac.Contains(mac));
            }

            if (!String.IsNullOrEmpty(ip))
            {
                lectors = lectors.Where(m => m.Ip.Contains(ip));
            }

            if (!String.IsNullOrEmpty(nroSerie))
            {
                lectors = lectors.Where(m => m.NroSerie.Contains(nroSerie));
            }

            if (!String.IsNullOrEmpty(nroParte))
            {
                lectors = lectors.Where(m => m.NroParte.Contains(nroParte));
            }

            if (!String.IsNullOrEmpty(marca))
            {
                lectors = lectors.Where(m => m.Marca.Contains(marca));
            }

            if (!String.IsNullOrEmpty(modelo))
            {
                lectors = lectors.Where(m => m.Modelo.Contains(modelo));
            }

            if (!String.IsNullOrEmpty(sucursal))
            {
                lectors = lectors.Where(m => m.Mac.Contains(mac));
            }

            return View(await lectors.ToListAsync());
        }



        //Para mostrar Vista para Crear.
        public IActionResult CrearLector()
        {
            ViewBag.Sucursals = new SelectList(_context.Sucursals, "IdSucursal", "Nombre");
            return View();
        }



        //Para crear Sector
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearLector(LectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Lector lector = new()
                {
                    Mac = model.Modelo,
                    Descripcion = model.Descripcion,
                    Ip = model.Ip,
                    NroSerie = model.NroSerie,
                    NroParte = model.NroParte,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    FkIdSucursal = model.FkIdSucursal,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };

                _context.Add(lector);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexLector));
            }

            ViewBag.Sucursals = new SelectList(_context.Sucursals, "IdSucursal", "Nombre", model.FkIdSucursal);
            return View(model);
        }



        //Para mostrar el Cargo a editar.
        public IActionResult EditarLector(int Id)
        {
            Lector lector = _context.Lectors.Find(Id);

            LectorViewModel model = new()
            {
                IdLector = lector.IdLector,
                Mac = lector.Mac,
                Descripcion = lector.Descripcion,
                Ip = lector.Ip,
                NroSerie = lector.NroSerie,
                NroParte = lector.NroParte,
                Marca = lector.Marca,
                Modelo = lector.Modelo,
                FkIdSucursal = lector.FkIdSucursal,
                Custom1 = lector.Custom1,
                Custom2 = lector.Custom2,
                Custom3 = lector.Custom3,
            };

            ViewBag.Sucursals = new SelectList(_context.Sucursals, "IdSucursal", "Nombre", model.FkIdSucursal);

            return View(model);
        }



        //Para editar SubSector.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarLector([Bind(include: "IdLector, Mac, Descripcion, Ip, NroSerie, NroParte, Marca, Modelo, FkIdSucursal, Custom1, Custom2, Custom3")] LectorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var lector = _context.Lectors.Find(model.IdLector);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                lector.Mac = model.Mac;
                lector.Descripcion = model.Descripcion;
                lector.Ip = model.Ip;
                lector.NroSerie = model.NroSerie;
                lector.NroParte = model.NroParte;
                lector.Marca = model.Marca;
                lector.Modelo = model.Modelo;
                lector.FkIdSucursal = model.FkIdSucursal;
                lector.Custom1 = model.Custom1;
                lector.Custom2 = model.Custom2;
                lector.Custom3 = model.Custom3;

                _context.Entry(lector).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexLector));
            }
            return View(model);
        }



        //Para confirmar eliminacion de SubSector.
        public IActionResult EliminarLector(int Id)
        {
            ViewBag.Errors = false;

            var lector = _context.Lectors.Find(Id);

            //Verificacion para no intentar borrar elementos asociados
            var antena = _context.Antenas.Where(m => m.FkIdLector.Equals(Id));
            var lectura = _context.Lecturas.Where(m => m.FkIdLector.Equals(Id));

            if (lectura.Any() || antena.Any())
            {
                ViewBag.Errors = true;
            }

            LectorViewModel model = new()
            {
                IdLector = lector.IdLector,
                Mac = lector.Mac,
                Descripcion = lector.Descripcion,
                Ip = lector.Ip,
                NroSerie = lector.NroSerie,
                NroParte = lector.NroParte,
                Marca = lector.Marca,
                Modelo = lector.Modelo,
                FkIdSucursal = lector.FkIdSucursal,
                Custom1 = lector.Custom1,
                Custom2 = lector.Custom2,
                Custom3 = lector.Custom3,
            };

            return View(model);
        }



        //Para eliminar Cargo.
        [HttpPost, ActionName("EliminarLector")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var lector = await _context.Lectors.FindAsync(id);
            _context.Lectors.Remove(lector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexLector));
        }
    }
}
