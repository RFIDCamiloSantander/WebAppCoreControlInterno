using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class SubSector
    {
        public SubSector()
        {
            Antenas = new HashSet<Antena>();
            Elementos = new HashSet<Elemento>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdSubSector { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Epc { get; set; }
        public int FkIdSector { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Sector FkIdSectorNavigation { get; set; }
        public virtual ICollection<Antena> Antenas { get; set; }
        public virtual ICollection<Elemento> Elementos { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
