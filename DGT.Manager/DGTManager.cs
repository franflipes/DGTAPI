using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.Entities;

namespace DGT.Manager
{
    public class DGTManager
    {
        public DGTManager()
        {
            CreateSeedEntities();
        }

        public List<Vehiculo> Vehiculos { get; set; }
        public List<Conductor> Conductores { get; set; }
        public List<Infraccion> Infracciones { get; set; }
        public List<InfraccionRegistrada> InfraccionesRegistradas { get; set; }
        public List<Marca> Marcas;

        private void CreateSeedEntities()
        {
            Marcas=SeedEntity.CrearMarcasModelos();
            Conductores=SeedEntity.CrearConductores();
            Vehiculos=SeedEntity.CrearVehiculos(Marcas,Conductores);
            Infracciones = SeedEntity.CrearInfracciones();
            InfraccionesRegistradas = new List<InfraccionRegistrada>();
        }

        #region Infracciones

        public Infraccion NuevaInfraccion(string identificador, string descripción, int puntos)
        {
            try
            {
                //Si existe la infraccion
                if (Infracciones.Exists(X => X.Identificador == identificador))
                    return null;
                Infraccion i = new Infraccion() { Identificador = identificador, Descripcion = descripción, PuntosDescontar = puntos };
                Infracciones.Add(i);
                return i;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public bool RegistrarInfraccion(Infraccion i, Vehiculo v)
        {
            try
            {
                Conductor c = Conductores.Where(x => x.DNI == v.ConductoresHabituales.First().DNI).First();

                if (c != null)
                {
                    InfraccionRegistrada ir = new InfraccionRegistrada() { Matricula = v.Matricula, Dni = c.DNI, Fecha = DateTime.Now, Infraccion = i };
                    c.DescontarPuntos(i.PuntosDescontar);
                    InfraccionesRegistradas.Add(ir);
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        #endregion

        #region Conductor
        public Conductor CreateConductor(String dni, String nombre, String apellidos)
        {
            if (ExisteDNI(dni))
                return null;
            try
            {
                Conductor c = new Conductor(dni, nombre, apellidos);
                Conductores.Add(c);
                return c;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private bool EsConductorHabitualEn10Vehiculos(Conductor c)
        {
            int nVehiculos = 0;
            Vehiculos.ForEach(
                    (v) =>
                    {
                        nVehiculos+=v.ConductoresHabituales.Where(ch => ch.DNI == c.DNI).Count();
                    }
                );

            if (nVehiculos >= 10)
                return true;

            return false;
        }


        //generar dni hasta que encontremos uno no repetido
        public String GetFreeDNI()
        {
            bool repetido = true;
            String dni="";
            while(repetido)
            {
                dni=Conductor.RandomDni();
                repetido = ExisteDNI(dni);
            }
            return dni;
            
        }

        private bool ExisteDNI(String dni)
        {
            if (Conductores.Any(x => x.DNI == dni))
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Vehiculo
        public Vehiculo CrearVehiculo(String matricula, String marca, String modelo,string dni)
        {

            try
            {
                //Si existe matricula ERROR
                if (ExisteMatricula(matricula))
                throw new Exception("Matricula Existe");

                //Si no existe dni ERROR
                Conductor c = Conductores.Where(x => x.DNI == dni).First();
                if(c==null)
                    throw new Exception("DNI no Existe");
           
                Vehiculo v = new Vehiculo(matricula, marca, modelo);


                //La descripcion no especifica si el conductor ya es habitual 10 veces, si el vehiculo se añade(sin conductor) o no. 
                //Yo decido añadirlo solo si puede ser habital
                if (AñadirConductorHabitual(v, c))
                {
                    Vehiculos.Add(v);
                    return v;
                }

                return null;
                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private bool AñadirConductorHabitual(Vehiculo v, Conductor c)
        {
            try
            {
                if (EsConductorHabitualEn10Vehiculos(c))
                    return false;

                Vehiculos.Where(veh => veh == v).First().ConductoresHabituales.Add(c);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public bool ExisteMatricula(String matricula)
        {
            if (Vehiculos.Any(x => x.Matricula == matricula))
            {
                return true; ;
            }
            return false;
        }
        #endregion
    }
}
