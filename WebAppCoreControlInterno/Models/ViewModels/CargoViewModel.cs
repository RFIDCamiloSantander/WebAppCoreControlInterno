using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class CargoViewModel
    {
        [Display(Name = "Id")]
        public int IdCargo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es Requerido.")]
        public string Cargo1 { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
