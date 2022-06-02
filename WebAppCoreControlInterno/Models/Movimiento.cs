using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Movimiento
    {
        public Movimiento()
        {
            Historials = new HashSet<Historial>();
        }

        public int IdMovimiento { get; set; }
        public int FkIdElemento { get; set; }
        public int FkIdSucursal { get; set; }
        public int? FkIdSector { get; set; }
        public int? FkIdSubSector { get; set; }
        public DateTime Fecha { get; set; }
        public string Epc { get; set; }

        public virtual Elemento FkIdElementoNavigation { get; set; }
        public virtual Sector FkIdSectorNavigation { get; set; }
        public virtual SubSector FkIdSubSectorNavigation { get; set; }
        public virtual Sucursal FkIdSucursalNavigation { get; set; }
        public virtual ICollection<Historial> Historials { get; set; }
    }
}
