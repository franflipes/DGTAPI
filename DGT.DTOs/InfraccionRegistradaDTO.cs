using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.DTOs
{
    public class InfraccionRegistradaDTO
    {
        public InfraccionDTO infraccion { get; set; }
        public VehiculoDTO vehiculo { get; set; }
    }
}
