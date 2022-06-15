using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class LectorViewModel
    {
        public LectorViewModel()
        {
            Antenas = new HashSet<Antena>();
            Lecturas = new HashSet<Lectura>();
        }

        public int IdLector { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Mac { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Ip { get; set; }
        public string NroSerie { get; set; }
        public string NroParte { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        [Display(Name = "Sucursal")]
        public int FkIdSucursal { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Sucursal FkIdSucursalNavigation { get; set; }
        public virtual ICollection<Antena> Antenas { get; set; }
        public virtual ICollection<Lectura> Lecturas { get; set; }
    }
}
