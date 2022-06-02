using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Historial
    {
        public int IdHistorial { get; set; }
        public DateTime Fecha { get; set; }
        public int IdElemento { get; set; }
        public int IdSucursal { get; set; }
        public int IdSector { get; set; }
        public int? IdSubSector { get; set; }
        public int FkIdMovimiento { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Movimiento FkIdMovimientoNavigation { get; set; }
    }
}
