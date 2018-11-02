using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.Manager;
using DGT.Entities;

namespace DGT.Services
{
    public interface IVehiculosService
    {
        List<Vehiculo> GetVehiculos();
        Vehiculo GetVehiculos(Func<Vehiculo, bool> where);
        Vehiculo CrearVehiculo(String matricula, String marca, String modelo, string dni);

        List<Marca> GetMarcas();
    }

    public class VehiculosService:IVehiculosService
    {
        #region ctr
        private readonly DGTManager _dgtManager;

        public VehiculosService()
        {
            _dgtManager = null;
        }

        public VehiculosService(DGTManager dgtManager)
        {
            _dgtManager = dgtManager;
        }

        #endregion

        public Vehiculo CrearVehiculo(string matricula, string marca, string modelo, string dni)
        {
            return _dgtManager.CrearVehiculo(matricula, marca, modelo, dni);
        }

        public List<Vehiculo> GetVehiculos()
        {
            return _dgtManager.Vehiculos;
        }

        public List<Marca> GetMarcas()
        {
            return _dgtManager.Marcas;
        }

        public Vehiculo GetVehiculos(Func<Vehiculo, bool> where)
        {
            return _dgtManager.Vehiculos.Where(where).FirstOrDefault();
        }
    }
}
