using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.DTOs
{
    public class InfraccionDTO
    {
        public InfraccionDTO()
        {

        }

        public String Identificador { get; set; }
        public String Descripcion { get; set; }
        public int PuntosDescontar { get; set; }
    }
}
