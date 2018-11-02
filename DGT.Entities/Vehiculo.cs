using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace DGT.Entities
{
    public class Vehiculo
    {
        public Vehiculo(String matricula, String modelo, string marca)
        {
            _matricula = matricula;
            Modelo = modelo;
            Marca = marca;
            ConductoresHabituales=new List<Conductor>();
        }


        private String _matricula;
        public String Matricula 
        { 
            get
            {
                return _matricula;
            }
        }

        public String Modelo { get; set; }
        
        public String Marca { get; set; }


        public List<Conductor> ConductoresHabituales { get; set; }


        public static string RandomMAtricula()
        {
            Random random = new Random();

            const string nums = "0123456789";
            string a = new string(Enumerable.Repeat(nums, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string b = new string(Enumerable.Repeat(chars, 3)
               .Select(s => s[random.Next(s.Length)]).ToArray());

            //Nos dormimos 1 segundo xq al hacerse todo dentro del mismo segundo la funcion random siempre nos dara los mismos resultados y por consiguiente la misma matricula siempre
            Thread.Sleep(1000);
            return a + b;
        }

    }
}
