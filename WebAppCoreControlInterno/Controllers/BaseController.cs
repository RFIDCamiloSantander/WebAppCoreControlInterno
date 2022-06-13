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
    public class BaseController : Controller
    {
        private readonly ControlInternoContext _context;

        public BaseController(ControlInternoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> IndexBase()
        {
            return View(await _context.Bases.ToListAsync());
        }


        //Pantalla creacion de elementos base
        public IActionResult CrearBase()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearBase(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {

                var bases = new Base()
                {
                    Sku = model.Sku,
                    Nombre = model.Nombre,
                    FkIdCategoria = model.FkIdCategoria,
                    Descripcion = model.Descripcion,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };
                _context.Add(bases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexBase));
            }

            return View(model);
        }



        //Para mostrar el Elemento Base a editar.
        public IActionResult EditarBase(int Id)
        {

            var tBase = _context.Bases.Find(Id);

            BaseViewModel model = new()
            {
                IdBase = tBase.IdBase,
                Nombre = tBase.Nombre,
                Sku = tBase.Sku,
                FkIdCategoria = tBase.FkIdCategoria,
                Descripcion = tBase.Descripcion,
                Custom1 = tBase.Custom1,
                Custom2 = tBase.Custom2,
                Custom3 = tBase.Custom3,
            };

            return View(model);
        }



        //Para editar el Elemento Base.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarBase( BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tBase = _context.Bases.Find(model.IdBase);

                tBase.Nombre = model.Nombre;
                tBase.Sku = model.Sku;
                tBase.FkIdCategoria = model.FkIdCategoria;
                tBase.Descripcion = model.Descripcion;
                tBase.Custom1 = model.Custom1;
                tBase.Custom2 = model.Custom2;
                tBase.Custom3 = model.Custom3;

                _context.Entry(tBase).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexBase));
            }

            return View(model);
        }



        //Para confirmar eliminacion de Elemento Base.
        public IActionResult EliminarBase(int Id)
        {

            var tBase = _context.Bases.Find(Id);

            BaseViewModel model = new()
            {
                IdBase = tBase.IdBase,
                Nombre = tBase.Nombre,
                Sku = tBase.Sku,
                FkIdCategoria = tBase.FkIdCategoria,
                Descripcion = tBase.Descripcion,
                Custom1 = tBase.Custom1,
                Custom2 = tBase.Custom2,
                Custom3 = tBase.Custom3,
            };

            return View(model);
        }


        //Para eliminar Elemento Base.
        [HttpPost, ActionName("EliminarBase")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var tBase = await _context.Bases.FindAsync(id);
            _context.Bases.Remove(tBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexBase));
        }
    }
}