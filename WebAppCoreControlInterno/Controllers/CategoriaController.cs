using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using WebAppCoreControlInterno.Models.ViewModels;

namespace WebAppCoreControlInterno.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ControlInternoContext _context;

        public CategoriaController(ControlInternoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexCategoria(string categoria)
        {
            ViewBag.Categoria = categoria;
            var categorias = from m in _context.Categoria select m;

            if (!String.IsNullOrEmpty(categoria))
            {
                categorias = categorias.Where(m => m.Nombre.Contains(categoria));
            }

            return View( await categorias.ToListAsync() );
        }


        public async Task<IActionResult> CrearCategoria(CategoriaViewModel model)
        {
            var tCategoria = _context.Categoria.Where(m => m.Nombre.Equals(model.Nombre));

            if (tCategoria.Any())
            {
                ViewBag.ErrorMessage = "Ya existe una Categoría con este nombre";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Categoria categoria = new()
                {
                    Nombre = model.Nombre
                };

                _context.Add(categoria);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexCategoria));
            }
            return View(model);
        }

        //Para mostrar el Categoría a editar.
        public IActionResult EditarCategoria(int Id)
        {

            var tCategoria = _context.Categoria.Find(Id);

            CategoriaViewModel model = new()
            {
                IdCategoria = tCategoria.IdCategoria,
                Nombre = tCategoria.Nombre,
            };

            return View(model);
        }


        //Para editar Categoría.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCategoria([Bind(include: "IdCategoria, Nombre")] CategoriaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tCategoria = _context.Categoria.Find(model.IdCategoria);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                tCategoria.Nombre = model.Nombre;

                _context.Entry(tCategoria).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexCategoria));
            }
            return View(model);
        }


        //Para confirmar eliminacion de Categoría.
        public IActionResult EliminarCategoria(int Id)
        {
            var tCategoria = _context.Categoria.Find(Id);

            CategoriaViewModel model = new()
            {
                IdCategoria = tCategoria.IdCategoria,
                Nombre = tCategoria.Nombre,
            };

            return View(model);
        }


        //Para eliminar Categoría.
        [HttpPost, ActionName("EliminarCategoria")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexCategoria));
        }
    }
}
