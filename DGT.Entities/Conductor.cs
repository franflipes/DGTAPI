using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DGT.Entities
{
    public class Conductor
    {
        public Conductor(string dni, string nombre, string apellidos)
        {
            if (!Regex.IsMatch(dni, "/[0 - 9]{ 7,8}[A-Z]/"))
                throw new Exception("Dni erroneo;");
            _dni = dni;
            Nombre = nombre;
            Apellidos = apellidos;
            _puntos = 15;

        }

        public Conductor()
        {
        }

        //This method is only use for the Seed
        public Conductor FillPropertiesInSeed(string dni, string nombre, string apellidos)
        {
            _dni = dni;
            Nombre = nombre;
            Apellidos = apellidos;
            _puntos = 15;

            return this;
        }

        private String _dni;
        public String DNI
        {
            get { return _dni; }
        }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }

        private int _puntos; 
        public int Puntos { get { return _puntos; } }

        public int DescontarPuntos(int puntos)
        {
            _puntos -= puntos;
            return Puntos;
        }

        public static string RandomDni()
        {
             Random random = new Random();

            const string nums = "0123456789";
            string a= new string(Enumerable.Repeat(nums, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string b = new string(Enumerable.Repeat(chars, 1)
               .Select(s => s[random.Next(s.Length)]).ToArray());

            return a + b;
        }
    }
}
