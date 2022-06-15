using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class SubSectorViewModel
    {
        public SubSectorViewModel()
        {
            Antenas = new HashSet<Antena>();
            Elementos = new HashSet<Elemento>();
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdSubSector { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Epc { get; set; }

        [Required(ErrorMessage = "El {0} es Requerido.")]
        [Display(Name = "Sector")]
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
