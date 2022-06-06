using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Elementos = new HashSet<Elemento>();
        }

        public int IdEmpleado { get; set; }
        public string Rut { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Epc { get; set; }
        public string Fotografia { get; set; }
        public string Contrasena { get; set; }
        public int? FkIdCargo { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Cargo FkIdCargoNavigation { get; set; }
        public virtual ICollection<Elemento> Elementos { get; set; }
    }
}
