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
    public class AntenaController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;


        //Se asigna el conexto(BD) a la variable
        public AntenaController(ControlInternoContext context)
        {
            _context = context;
        }



        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexAntena(string nombre)
        {
            //var antenas = from m in _context.Antenas select m;

            //if (!String.IsNullOrEmpty(nombre))
            //{
            //    //antenas = antenas.Where(m => m.NumAntena.Contains(nombre));
            //}

            //return View(await antenas.Include(b => b.FkIdLectorNavigation).ToListAsync());

            return View( await _context.Antenas.ToListAsync());
        }



        //Para mostrar Vista para Crear.
        public IActionResult CrearAntena()
        {
            ViewBag.Sectors = new SelectList(_context.Sectors, "IdSector", "Nombre");
            ViewBag.SubSectors = new SelectList(_context.SubSectors, "IdSubSector", "Nombre");
            ViewBag.Lectors = new SelectList(_context.Lectors, "IdLector", "Mac");
            return View();
        }



        //Para crear Sector
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAntena(AntenaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Antena antena= new()
                {
                    NumAntena = model.NumAntena,
                    FkIdSector = model.FkIdSector,
                    FkIdSubSector = model.FkIdSubSector,
                    FkIdLector = model.FkIdLector,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };

                _context.Add(antena);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexAntena));
            }
            return View(model);
        }



        //Para mostrar el Cargo a editar.
        public IActionResult EditarAntena(int Id)
        {
            Antena antena = _context.Antenas.Find(Id);

            AntenaViewModel model = new()
            {
                NumAntena = antena.NumAntena,
                FkIdSector = antena.FkIdSector,
                FkIdSubSector = antena.FkIdSubSector,
                FkIdLector = antena.FkIdLector,
                Custom1 = antena.Custom1,
                Custom2 = antena.Custom2,
                Custom3 = antena.Custom3,
            };

            return View(model);
        }



        //Para editar SubSector.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarAntena([Bind(include: "IdAntena, Antena1, FkIdSector, FkIdSubSector, FkIdLector, Custom1, Custom2, Custom3")] AntenaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var antena = _context.Antenas.Find(model.IdAntena);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                antena.NumAntena = model.NumAntena;
                antena.FkIdSector = model.FkIdSector;
                antena.FkIdSubSector = model.FkIdSubSector;
                antena.FkIdLector = model.FkIdLector;
                antena.Custom1 = model.Custom1;
                antena.Custom2 = model.Custom2;
                antena.Custom3 = model.Custom3;

                _context.Entry(antena).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexAntena));
            }
            return View(model);
        }



        //Para confirmar eliminacion de SubSector.
        public IActionResult EliminarAntena(int Id)
        {
            var antena = _context.Antenas.Find(Id);

            AntenaViewModel model = new()
            {
                IdAntena = antena.IdAntena,
                NumAntena = antena.NumAntena,
                FkIdSector = antena.FkIdSector,
                FkIdSubSector = antena.FkIdSubSector,
                FkIdLector = antena.FkIdLector,
                Custom1 = antena.Custom1,
                Custom2 = antena.Custom2,
                Custom3 = antena.Custom3,
            };

            return View(model);
        }



        //Para eliminar Cargo.
        [HttpPost, ActionName("EliminarAntena")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var antena = await _context.Antenas.FindAsync(id);
            _context.Antenas.Remove(antena);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAntena));
        }
    }
}
