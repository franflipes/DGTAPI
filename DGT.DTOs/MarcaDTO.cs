using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.DTOs
{
    public class MarcaDTO
    {
        public MarcaDTO()
        {
           
        }


        public String NombreMarca { get; set; }

        public IList<ModeloDTO> Modelos { get; set; }
    }
}
