using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel()
        {
            Bases = new HashSet<Base>();
        }

        [Display(Name = "ID")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Nombre { get; set; }

        public virtual ICollection<Base> Bases { get; set; }
    }
}
