using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.Entities;

namespace DGT.Manager
{
    public static class SeedEntity
    {
        private static String[] infracciones = { "Saltar semaforo.", "Limite de Velocidad.", "Mal Aparcamiento.","Obstruccion al paso." };

        public static List<Marca> CrearMarcasModelos()
        {
            try
            {
                List<Marca> marcas = new List<Marca>();

                var seat = new Marca("Seat") ;
                seat.Modelos.Add(new Modelo() { nombreModelo = "Arona"});
                seat.Modelos.Add(new Modelo() { nombreModelo = "Ibiza" });
                seat.Modelos.Add(new Modelo() { nombreModelo = "Cordoba" });

                marcas.Add(seat);

                var hiunday = new Marca("Hiunday");
                hiunday.Modelos.Add(new Modelo() { nombreModelo = "Accent" });
                hiunday.Modelos.Add(new Modelo() { nombreModelo = "Tucson"});
                hiunday.Modelos.Add(new Modelo() { nombreModelo = "Lantra"});

                marcas.Add(hiunday);

                var skoda = new Marca("Skoda");
                skoda.Modelos.Add(new Modelo() { nombreModelo = "Fabia" });
                skoda.Modelos.Add(new Modelo() { nombreModelo = "Octavia" });
                skoda.Modelos.Add(new Modelo() { nombreModelo = "Karoq"});

                marcas.Add(skoda);

                return marcas;


            }
            catch (Exception e)
            { throw e; }
            
        }

        internal static List<Infraccion> CrearInfracciones()
        {
            List<Infraccion> infracciones = new List<Infraccion>();
            for (int i = 0; i < 4; i++)
            {
                Infraccion inf = new Infraccion() { Identificador = "Inf-" + i.ToString(), Descripcion = SeedEntity.infracciones[i], PuntosDescontar = i + 1 };
                infracciones.Add(inf);
            }
            return infracciones;
        }

        public static List<Vehiculo> CrearVehiculos(List<Marca> marcas,List<Conductor> conductores)
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            for (int i = 0; i < 10; i++)
            {
                int nMarcas = marcas.Count();
                int nModelos = marcas.ElementAt(i % nMarcas).Modelos.Count();

                var marca = marcas.ElementAt(i % nMarcas);
                var modelo=marcas.ElementAt(i % nMarcas).Modelos.ElementAt(i % nModelos);
                var vehiculo = new Vehiculo(Vehiculo.RandomMAtricula(),modelo.nombreModelo,marca.NombreMarca);


                Random r = new Random();
                int nConductoresHabituales = r.Next(11);
                for (int j = 0; j < nConductoresHabituales; j++)
                {
                    Conductor c = conductores.ElementAt(i * j);
                    
                    if (c != null && 
                        !SeedEntity.EsConductorHabitualEn10Vehiculos(c,vehiculos) && 
                        !vehiculo.ConductoresHabituales.Exists(x=>x==c))
                    {
                        vehiculo.ConductoresHabituales.Add(c);
                    }

                }
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }

        public  static List<Conductor> CrearConductores()
        {
            List<Conductor> conductores = new List<Conductor>();
            List<String> dnis = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                var dni = GetFreeDNI(dnis);
                dnis.Add(dni);
                try
                {
                    var conductor = new Conductor();
                    conductor.FillPropertiesInSeed(dni, "Nombre" + i, "Apellidos" + i);
                    conductores.Add(conductor);
                }
                catch (Exception e)
                { 
                //Nos tragamos la exception en este caso para no para la creacion de Entidades
                }
                

                
            }
            return conductores;
        }

        //generar dni hasta que encontremos uno no repetido
        public static String GetFreeDNI(List<string> dnis)
        {
            bool repetido = true;
            String dni = "";
            while (repetido)
            {
                dni = Conductor.RandomDni();
                repetido = dnis.Exists(x => x == dni);
            }
            return dni;

        }

        private static bool EsConductorHabitualEn10Vehiculos(Conductor c, List<Vehiculo> vehiculos)
        {
            int nVehiculos = 0;
            vehiculos.ForEach(
                    (v) =>
                    {
                        nVehiculos += v.ConductoresHabituales.Where(ch => ch.DNI == c.DNI).Count();
                    }
                );

            if (nVehiculos >= 10)
                return true;

            return false;
        }
    }
}
