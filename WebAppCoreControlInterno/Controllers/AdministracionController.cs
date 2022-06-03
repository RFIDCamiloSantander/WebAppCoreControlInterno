using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

namespace WebAppCoreControlInterno.Controllers
{
    public class AdministracionController : Controller
    {
        //Se crea la variable para el contexto
        private readonly ControlInternoContext _context;

        //Se obtiene el context del controlador y se asigna a la variable
        public AdministracionController(ControlInternoContext context)
        {
            _context = context;
        }

        //Para obtener los datos de la
        public IActionResult Index()
        {
            return View();
        }
    }
}
