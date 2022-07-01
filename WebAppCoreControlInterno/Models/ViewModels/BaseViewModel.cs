using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            Elementos = new HashSet<Elemento>();
        }


        public int IdBase { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Nombre { get; set; }
        
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public int? FkIdCategoria { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual ICollection<Elemento> Elementos { get; set; }
    }
}
