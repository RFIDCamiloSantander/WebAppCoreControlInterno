using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class EmpleadoViewModel
    {
        [Display(Name = "ID")]
        public int IdEmpleado { get; set; }
        public string Rut { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Primer Nombre")]
        public string Nombre1 { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string Nombre2 { get; set; }
        [Required]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        public string Epc { get; set; }
        [Display(Name = "Fotografía")]
        public string Fotografia { get; set; }
        [Required]
        [Display(Name = "Cargo")]
        public int? FkIdCargo { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
    }
}
