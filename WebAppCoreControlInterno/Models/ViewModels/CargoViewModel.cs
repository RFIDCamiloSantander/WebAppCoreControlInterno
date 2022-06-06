using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class CargoViewModel
    {
        [Display(Name = "Id")]
        public int IdCargo { get; set; }

        [Display(Name = "Nombre Cargo")]
        [Required]
        public string Cargo1 { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
