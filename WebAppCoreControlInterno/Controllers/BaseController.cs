using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

namespace WebAppCoreControlInterno.Controllers
{
    public class BaseController : Controller
    {
        private readonly ControlInternoContext _context;

        public BaseController( ControlInternoContext context )
        {
            _context = context;
        }


        public async Task<IActionResult> IndexBase()
        {
            return View( await _context.Bases.ToListAsync());
        }



    }
}