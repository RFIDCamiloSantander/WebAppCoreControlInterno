using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppCoreControlInterno.Models.ViewModels
{
    public class CargoViewModel
    {
        public int IdCargo { get; set; }

        [Required]
        public string Cargo1 { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
