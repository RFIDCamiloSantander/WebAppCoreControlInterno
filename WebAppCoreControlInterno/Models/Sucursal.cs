using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Elementos = new HashSet<Elemento>();
            Lectors = new HashSet<Lector>();
            Movimientos = new HashSet<Movimiento>();
            Sectors = new HashSet<Sector>();
        }

        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
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
