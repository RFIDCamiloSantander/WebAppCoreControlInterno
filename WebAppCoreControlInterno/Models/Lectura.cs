using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Lectura
    {
        public int IdLectura { get; set; }
        public string Epc { get; set; }
        public DateTime PrimeraVista { get; set; }
        public DateTime UltimaVista { get; set; }
        public int Rssi { get; set; }
        public byte Antenna { get; set; }
        public int FkIdLector { get; set; }
        public int FkIdElemento { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Elemento FkIdElementoNavigation { get; set; }
        public virtual Lector FkIdLectorNavigation { get; set; }
    }
}
