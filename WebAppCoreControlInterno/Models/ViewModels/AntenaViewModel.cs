using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class AntenaViewModel
    {
        public int IdAntena { get; set; }

        [Required]
        [Display(Name = "N°Antena")]
        public byte NumAntena { get; set; }

        [Display(Name = "Sector")]
        public int? FkIdSector { get; set; }

        [Display(Name = "SubSector")]
        public int? FkIdSubSector { get; set; }

        [Required]
        [Display(Name = "Lector")]
        public int FkIdLector { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Lector FkIdLectorNavigation { get; set; }
        public virtual Sector FkIdSectorNavigation { get; set; }
        public virtual SubSector FkIdSubSectorNavigation { get; set; }
    }
}
