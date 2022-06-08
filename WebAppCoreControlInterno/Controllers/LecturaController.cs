using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Impinj;
using WebAppCoreControlInterno.Models;

namespace WebAppCoreControlInterno.Controllers
{
    public class LecturaController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;

        //Se asigna el conexto(BD) a la variable
        public LecturaController(ControlInternoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexLectura()
        {
            return View(await _context.Lecturas.ToListAsync());
        }


        public IActionResult CrearLectura()
        {
            Reader r = new();
            r.CrearLectura();
            return RedirectToAction(nameof(Index));
        }
    }
}
