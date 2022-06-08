using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Impinj;
using WebAppCoreControlInterno.Models.ViewModels;

namespace WebAppCoreControlInterno.Controllers
{
    public class LeerController : Controller
    {
        [Route("Ruta1")]
        public IActionResult IndexLeer()
        {
            return View();
        }

        
        public IActionResult IndexLeer( LeerViewModel m )
        {
            Reader r = new();

            m.Data = r.CrearLectura().Result;
            return View(m);
        }
    }
}
