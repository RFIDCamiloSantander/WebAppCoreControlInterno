using Impinj.OctaneSdk;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using Newtonsoft.Json;
using WebAppCoreControlInterno.Models.ViewModels;
using WebAppCoreControlInterno.Impinj;

namespace WebAppCoreControlInterno.Controllers
{
    public class ReaderController : Controller
    {
        //Se crea la variable para el conexto (BD)
        private readonly ControlInternoContext _context;

        //Se asigna el conexto(BD) a la variable
        public ReaderController(ControlInternoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _context.Lecturas.ToListAsync() );
        }


        public IActionResult CrearLectura()
        {
            Reader r = new();
            r.CrearLectura();
            return RedirectToAction(nameof(Index));
        }
    }
}
