using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppCoreControlInterno.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Elementos = new HashSet<Elemento>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; }

        public virtual ICollection<Elemento> Elementos { get; set; }
    }
}
