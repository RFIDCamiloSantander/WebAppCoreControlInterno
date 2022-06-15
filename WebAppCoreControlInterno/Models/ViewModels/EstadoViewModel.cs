using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class EstadoViewModel
    {
        public EstadoViewModel()
        {
            Elementos = new HashSet<Elemento>();
        }

        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        [Display(Name = "Estado")]
        public string Estado1 { get; set; }

        public virtual ICollection<Elemento> Elementos { get; set; }
    }
}
