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
    public class CargoController : Controller
    {

        private readonly ControlInternoContext _context;

        public CargoController( ControlInternoContext context )
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _context.Cargos.ToListAsync());
        }

        //Chequeando que hace
        public IActionResult Create()
        {
            ViewData["Cargos"] = new SelectList(_context.Cargos, "");
            return View();
        }

        //Para crear empleados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CargoViewModel model )
        {
            if ( ModelState.IsValid )
            {
                Cargo cargo = new Cargo()
                {
                    Cargo1 = model.Cargo1
                };

                _context.Add( cargo );

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Cargos"] = new SelectList(_context.Cargos, "");
            return View( model );
        }
    }
}
