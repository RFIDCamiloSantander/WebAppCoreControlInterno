using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Lector
    {
        public Lector()
        {
            Antenas = new HashSet<Antena>();
            Lecturas = new HashSet<Lectura>();
        }

        public int IdLector { get; set; }
        public string Mac { get; set; }
        public string Descripcion { get; set; }
        public string Ip { get; set; }
        public string NroSerie { get; set; }
        public string NroParte { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int FkIdSucursal { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Sucursal FkIdSucursalNavigation { get; set; }
        public virtual ICollection<Antena> Antenas { get; set; }
        public virtual ICollection<Lectura> Lecturas { get; set; }
    }
}
