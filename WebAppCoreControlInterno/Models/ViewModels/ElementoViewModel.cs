using System;
using System.Collections.Generic;
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
        public string FechaIngreso { get; set; }
        public string FechaUltimaLectura { get; set; }
        public string Dimension { get; set; }
        public double? PesoKg { get; set; }
        public string NroSerie { get; set; }
        public string NroParte { get; set; }
        public string InformacionExtra { get; set; }
        public string Fotografia { get; set; }
        public int FkIdElementoBase { get; set; }
        public int FkIdEstado { get; set; }
        public int FkIdSucursal { get; set; }
        public int? FkIdSector { get; set; }
        public int? FkIdSubSector { get; set; }
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
