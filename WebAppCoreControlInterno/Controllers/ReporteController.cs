using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;

namespace WebAppCoreControlInterno.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ControlInternoContext _context;

        public ReporteController(ControlInternoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexReporte()
        {
            //ViewBag.Elementos = await _context.Elementos.ToListAsync();
            return View();
        }
    }
}
