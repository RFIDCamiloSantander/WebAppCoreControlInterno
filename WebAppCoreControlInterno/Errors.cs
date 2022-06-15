using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreControlInterno
{
    public class Errors
    {
        public static string minLength { get; set; }

        public Errors() {
            minLength = "El {0} debe tener al menos {1} caracteres.";
        }
    }
}
