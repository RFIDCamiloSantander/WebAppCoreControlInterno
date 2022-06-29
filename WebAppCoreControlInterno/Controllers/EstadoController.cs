using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using WebAppCoreControlInterno.Models.ViewModels;
using Newtonsoft.Json;

namespace WebAppCoreControlInterno.Controllers
{
    public class EstadoController : Controller
    {

        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;

        //Se asigna el conexto(BD) a la variable
        public EstadoController(ControlInternoContext context)
        {
            _context = context;
        }



        //Pantalla Index (Listar Estados)
        public async Task<IActionResult> IndexEstado( string nombre )
        {
            ViewBag.Nombre = nombre;

            var estados = from m in _context.Estados select m;

            if (!String.IsNullOrEmpty(nombre))
            {
                estados = estados.Where(m => m.Estado1.Contains(nombre));
            }

            return View( await estados.ToListAsync() );
        }



        //Carga pantalla para crear Estados
        public IActionResult CrearEstado()
        {
            return View();
        }



        //Si el Formulario es valido, agrega el Estado a la BD
        //Si no es valido, retorna a la pantalla de creacion con los datos cargados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEstado(EstadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Estado estado = new Estado()
                {
                    Estado1 = model.Estado1
                };

                _context.Add(estado);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexEstado));
            }

            return View(model);
        }



        //Pantalla para editar Estados, cargando el estado que se desea editar.
        public IActionResult EditarEstado(int Id)
        {
            var tEstado = _context.Estados.Find(Id);

            EstadoViewModel model = new EstadoViewModel();
            model.IdEstado = tEstado.IdEstado;
            model.Estado1 = tEstado.Estado1;

            return View(model);
        }



        //Si el Formulario es valido, edita el Estado a la BD
        //Si no es valido, retorna a la pantalla de edición con los datos cargados.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarEstado([Bind(include: "IdEstado, Estado1")] EstadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tEstado = _context.Estados.Find(model.IdEstado);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                tEstado.Estado1 = model.Estado1;

                _context.Entry(tEstado).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexEstado));
            }
            return View(model);
        }



        //Pantalla de confirmacion de eliminacion de Estado.
        public IActionResult EliminarEstado(int Id)
        {
            ViewBag.Errors = false;
            var tElemento = _context.Elementos.Where(m => m.FkIdEstado.Equals(Id));

            if (tElemento.Any())
            {
                ViewBag.Errors = true;
            }

            var tEstado = _context.Estados.Find(Id);

            EstadoViewModel model = new()
            {
                IdEstado = tEstado.IdEstado,
                Estado1 = tEstado.Estado1,
            };

            return View(model);
        }



        //Eliminación de estado previa confirmación.
        [HttpPost, ActionName("EliminarEstado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarEstadoConfirmado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexEstado));
        }
    }
}
