using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

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
    }
}
