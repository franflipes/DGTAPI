using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGT.Entities
{
    public class Marca
    {
        public Marca(String nombreMarca)
        {
            _nombreMarca = nombreMarca;
            _modelos = new List<Modelo>();
        }

        private String _nombreMarca;
        public String NombreMarca { get { return _nombreMarca; } }

        private List<Modelo> _modelos;
        public IList<Modelo> Modelos { get { return _modelos; } }
    }
}
