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
    public class SucursalController : Controller
    {
        private readonly ControlInternoContext _context;

        //Se obtiene el context del controlador
        public SucursalController(ControlInternoContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> IndexSucursal()
        {
            return View( await _context.Sucursals.ToListAsync() );
        }



        //Para mostrar Vista para Crear.
        public IActionResult CrearSucursal()
        {
            return View();
        }



        //Para crear Sucursal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSucursal(SucursalViewModel model)
        {
            if (ModelState.IsValid)
            {
                Sucursal sucursal = new()
                {
                    Nombre = model.Nombre,
                    Direccion = model.Direccion,
                    Region = model.Region,
                    Comuna = model.Comuna,
                    Epc = model.Epc,
                    CoordenadaX = model.CoordenadaX,
                    CoordenadaY = model.CoordenadaY,
                    Custom1 = model.Custom1,
                    Custom2 = model.Custom2,
                    Custom3 = model.Custom3,
                };

                _context.Add(sucursal);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexSucursal));
            }
            return View(model);
        }


        //Para mostrar la Sucursal a editar.
        public IActionResult EditarSucursal(int Id)
        {
            var tSucursal = _context.Sucursals.Find(Id);

            SucursalViewModel model = new()
            {
                IdSucursal = tSucursal.IdSucursal,
                Nombre = tSucursal.Nombre,
                Direccion = tSucursal.Direccion,
                Region = tSucursal.Region,
                Comuna = tSucursal.Comuna,
                Epc = tSucursal.Epc,
                CoordenadaX = tSucursal.CoordenadaX,
                CoordenadaY = tSucursal.CoordenadaY,
                Custom1 = tSucursal.Custom1,
                Custom2 = tSucursal.Custom2,
                Custom3 = tSucursal.Custom3,
            };

            return View(model);
        }


        //Para editar Cargo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarSucursal(
            [Bind(
                include: "IdSucursal, Nombre, Direccion, Region, Comuna, Epc, CoordenadaX, CoordenadaY, Custom1, Custom2, Custom3"
            )] SucursalViewModel model
        )
        {
            if (ModelState.IsValid)
            {
                var tSucursal = _context.Sucursals.Find(model.IdSucursal);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                tSucursal.Nombre = model.Nombre;
                tSucursal.Direccion = model.Direccion;
                tSucursal.Region = model.Region;
                tSucursal.Comuna = model.Comuna;
                tSucursal.Epc = model.Epc;
                tSucursal.CoordenadaX = model.CoordenadaX;
                tSucursal.CoordenadaY = model.CoordenadaY;
                tSucursal.Custom1 = model.Custom1;
                tSucursal.Custom2 = model.Custom2;
                tSucursal.Custom3 = model.Custom3;

                _context.Entry(tSucursal).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexSucursal));
            }
            return View(model);
        }


        //Para confirmar eliminacion de Sucursal.
        public IActionResult EliminarSucursal(int Id)
        {
            var tSucursal = _context.Sucursals.Find(Id);

            SucursalViewModel model = new()
            {
                IdSucursal = tSucursal.IdSucursal,
                Nombre = tSucursal.Nombre,
                Direccion = tSucursal.Direccion,
                Region = tSucursal.Region,
                Comuna = tSucursal.Comuna,
                Epc = tSucursal.Epc,
                CoordenadaX = tSucursal.CoordenadaX,
                CoordenadaY = tSucursal.CoordenadaY,
                Custom1 = tSucursal.Custom1,
                Custom2 = tSucursal.Custom2,
                Custom3 = tSucursal.Custom3,
            };

            return View(model);
        }


        //Para eliminar Sucursal.
        [HttpPost, ActionName("EliminarSucursal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var sucursal = await _context.Sucursals.FindAsync(id);
            _context.Sucursals.Remove(sucursal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSucursal));
        }
    }
}
