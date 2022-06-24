using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Bases = new HashSet<Base>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Base> Bases { get; set; }
    }
}
