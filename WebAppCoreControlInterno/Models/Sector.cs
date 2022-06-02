using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Sector
    {
        public Sector()
        {
            Antenas = new HashSet<Antena>();
            Elementos = new HashSet<Elemento>();
            Movimientos = new HashSet<Movimiento>();
            SubSectors = new HashSet<SubSector>();
        }

        public int IdSector { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int FkIdSucursal { get; set; }

        public virtual Sucursal FkIdSucursalNavigation { get; set; }
        public virtual ICollection<Antena> Antenas { get; set; }
        public virtual ICollection<Elemento> Elementos { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
        public virtual ICollection<SubSector> SubSectors { get; set; }
    }
}
