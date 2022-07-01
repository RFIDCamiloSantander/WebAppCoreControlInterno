using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class SucursalViewModel
    {
        public SucursalViewModel()
        {
            Elementos = new HashSet<Elemento>();
            Lectors = new HashSet<Lector>();
            Movimientos = new HashSet<Movimiento>();
            Sectors = new HashSet<Sector>();
        }

        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La {0} es Requerida.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La {0} es Requerida.")]
        [Display(Name = "Región")]
        public string Region { get; set; }
        public string Comuna { get; set; }
        public string Epc { get; set; }
        public int? CoordenadaX { get; set; }
        public int? CoordenadaY { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual ICollection<Elemento> Elementos { get; set; }
        public virtual ICollection<Lector> Lectors { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
        public virtual ICollection<Sector> Sectors { get; set; }
    }
}
