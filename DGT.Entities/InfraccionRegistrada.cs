using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.Entities
{
    public class InfraccionRegistrada
    {
       public DateTime Fecha { get; set; }
       public Infraccion Infraccion { get; set; }
       public  String Matricula { get; set; }
       public String Dni { get; set; }
    }
}
