using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class AntenaViewModel
    {
        public int IdAntena { get; set; }
        public byte Antena1 { get; set; }
        public int? FkIdSector { get; set; }
        public int? FkIdSubSector { get; set; }
        public int FkIdLector { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }

        public virtual Lector FkIdLectorNavigation { get; set; }
        public virtual Sector FkIdSectorNavigation { get; set; }
        public virtual SubSector FkIdSubSectorNavigation { get; set; }
    }
}
