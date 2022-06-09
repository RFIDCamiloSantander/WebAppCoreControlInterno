using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

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


        public async Task<IActionResult> IndexEstado()
        {
            return View( await _context.Estados.ToListAsync() );
        }
    }
}
