using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class ElementoViewModel
    {
        public ElementoViewModel()
        {
            Lecturas = new HashSet<Lectura>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdElemento { get; set; }
        public string Epc { get; set; }

        [Required]
        [Display(Name = "Fecha Ingreso")]
        public string FechaIngreso { get; set; }

        [Required]
        [Display(Name = "Fecha Ultima Lectura")]
        public string FechaUltimaLectura { get; set; }
        public string Dimension { get; set; }
        public double? PesoKg { get; set; }
        public string NroSerie { get; set; }
        public string NroParte { get; set; }
        public string InformacionExtra { get; set; }
        public string Fotografia { get; set; }

        [Display(Name = "Elemento Base")]
        public int FkIdElementoBase { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int FkIdEstado { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public int FkIdSucursal { get; set; }

        [Display(Name = "Sector")]
        public int? FkIdSector { get; set; }

        [Display(Name = "SubSector")]
        public int? FkIdSubSector { get; set; }

        [Display(Name = "Encargado")]
        public int? FkIdEmpleadoEncargado { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Base FkIdElementoBaseNavigation { get; set; }
        public virtual Empleado FkIdEmpleadoEncargadoNavigation { get; set; }
        public virtual Estado FkIdEstadoNavigation { get; set; }
        public virtual Sector FkIdSectorNavigation { get; set; }
        public virtual SubSector FkIdSubSectorNavigation { get; set; }
        public virtual Sucursal FkIdSucursalNavigation { get; set; }
        public virtual ICollection<Lectura> Lecturas { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
