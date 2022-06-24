using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAppCoreControlInterno.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAppCoreControlInterno.Models.ViewModels;
using Newtonsoft.Json;

namespace WebAppCoreControlInterno.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ControlInternoContext _context;

        public ReporteController(ControlInternoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexReporte(string pBase, string serie, string parte, string sucursal)
        {
            ViewBag.Base = pBase;
            ViewBag.Serie = serie;
            ViewBag.Parte = parte;
            ViewBag.Sucursal = sucursal;
            var elementos = from m in _context.Elementos.
                            Include(m => m.FkIdElementoBaseNavigation).
                            Include(m => m.FkIdSucursalNavigation) select m;

            var sucursals = from m in _context.Sucursals select m;

            var categorias = from m in _context.Categoria select m;

            if (!String.IsNullOrEmpty(pBase))
            {
                elementos = elementos.Where(m => m.FkIdElementoBaseNavigation.Nombre.Contains(pBase));
            }

            if (!String.IsNullOrEmpty(serie))
            {
                elementos = elementos.Where(m => m.NroSerie.Contains(serie));
            }

            if (!String.IsNullOrEmpty(parte))
            {
                elementos = elementos.Where(m => m.NroParte.Contains(parte));
            }

            if (!String.IsNullOrEmpty(sucursal))
            {
                elementos = elementos.Where(m => m.FkIdSucursalNavigation.Nombre.Contains(sucursal));
            }

            ViewBag.Categorias = await categorias.ToListAsync();
            ViewBag.Elementos = await elementos.ToListAsync();
            ViewBag.Sucursals = await sucursals.ToListAsync();
            return View();
        }
    }
}
