﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class CargoController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;

        //Se asigna el conexto(BD) a la variable
        public CargoController(ControlInternoContext context)
        {
            _context = context;
        }


        //Para mostrar Vista principal.
        public async Task<IActionResult> IndexCargo( string cargo )
        {
            ViewBag.Cargo = cargo;

            var cargos = from m in _context.Cargos select m;

            if (!String.IsNullOrEmpty(cargo))
            {
                cargos = cargos.Where( m => m.Cargo1.Contains( cargo ) );
            }

            return View(await cargos.ToListAsync());
        }

        ////Para mostrar Vista para Crear.
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //Para crear empleados
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CargoViewModel model)
        {
            //System.Diagnostics.Debug.WriteLine( JsonConvert.SerializeObject(model) );

            var tCargo = _context.Cargos.Where(m => m.Cargo1.Equals(model.Cargo1));

            if (tCargo.Any())
            {
                ViewBag.ErrorMessage = "Ya existe un Cargo con este nombre";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                Cargo cargo = new()
                {
                    Cargo1 = model.Cargo1
                };

                _context.Add(cargo);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexCargo));
            }
            return View(model);
        }


        //Para mostrar el Cargo a editar.
        public IActionResult Editar(int Id)
        {
            var oCargo = _context.Cargos.Find(Id);

            CargoViewModel model = new()
            {
                IdCargo = oCargo.IdCargo,
                Cargo1 = oCargo.Cargo1,
            };
            

            return View(model);
        }


        //Para editar Cargo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar( [Bind(include:"IdCargo, Cargo1")] CargoViewModel model)
        {
            if ( ModelState.IsValid )
            {
                var tCargo = _context.Cargos.Find(model.IdCargo);

                //System.Diagnostics.Debug.WriteLine(model.IdCargo + " - el id que llega");
                tCargo.Cargo1 = model.Cargo1;

                _context.Entry(tCargo).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexCargo));
            }
            return View(model);
        }


        //Para confirmar eliminacion de Cargo.
        public IActionResult Eliminar(int Id)
        {
            ViewBag.Errors = false;
            var oCargo = _context.Cargos.Find(Id);
            var empleado = _context.Empleados.Where(m => m.FkIdCargo.Equals(Id));

            if (empleado.Any())
            {
                ViewBag.Errors = true;
            }

            CargoViewModel model = new()
            {
                IdCargo = oCargo.IdCargo,
                Cargo1 = oCargo.Cargo1,
            };


            return View(model);
        }


        //Para eliminar Cargo.
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexCargo));
        }
    }
}
