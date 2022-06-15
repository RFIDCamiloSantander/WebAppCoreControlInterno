using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class EmpleadoViewModel
    {
        
        public int IdEmpleado { get; set; }


        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Rut { get; set; }


        [Required(ErrorMessage = "El {0} es Requerido.")]
        [DataType(DataType.Text)]
        [Display(Name = "Primer Nombre")]
        public string Nombre1 { get; set; }


        [Display(Name = "Segundo Nombre")]
        public string Nombre2 { get; set; }


        [Required(ErrorMessage = "El {0} es Requerido.")]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }


        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }


        public string Epc { get; set; }


        [Display(Name = "Fotografía")]
        public string Fotografia { get; set; }


        [Compare("Contrasena2")]
        public string Contrasena { get; set; }

        [Compare("Contrasena")]
        public string Contrasena2 { get; set; }


        [Required(ErrorMessage = "El {0} es Requerido.")]
        //[DataType(DataType.Text, ErrorMessage = "Debe elegir un 'Cargo'.")]
        [Display(Name = "Cargo")]
        public int? FkIdCargo { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
    }
}
