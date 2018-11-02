using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DGT.DTOs
{
    public class ConductorDTO
    {
        
        public ConductorDTO()
        {
        }

        public String DNI { get; set; }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }
     
        public int Puntos { get; set; }

    }
}
