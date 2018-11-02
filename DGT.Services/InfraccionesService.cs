using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.Manager;
using DGT.Entities;

namespace DGT.Services
{
    public interface IInfraccionesService
    {
        bool RegistrarInfraccion(Infraccion i, String Matricula);
        Infraccion NuevaInfraccion( string identificador, string descripción, int puntos);
        IEnumerable<Infraccion> GetInfracciones();
        IEnumerable<Infraccion> GetTopInfracciones();
        IEnumerable<InfraccionRegistrada> GetInfraccionesPorDNI(String dni);

    }

    public class InfraccionesService:IInfraccionesService
    {

        #region ctr
        private readonly DGTManager _dgtManager;
        

        public InfraccionesService()
        {
            _dgtManager = null;
        }

        public InfraccionesService(DGTManager dgtManager)
        {
            _dgtManager = dgtManager;
        }

        public IEnumerable<Infraccion> GetInfracciones()
        {
            return _dgtManager.Infracciones;
        }

        public IEnumerable<InfraccionRegistrada> GetInfraccionesPorDNI(string dni)
        {
            return _dgtManager.InfraccionesRegistradas.Where(x => x.Dni == dni);
        }

        public IEnumerable<Infraccion> GetTopInfracciones()
        {
            var infraccionesGrouped = _dgtManager.InfraccionesRegistradas.GroupBy(ir => ir.Infraccion).Select(g => new { g.Key, Count = g.Count() });
            return infraccionesGrouped.OrderByDescending(x => x.Count).Take(5).Select(x=>x.Key);



        }

        #endregion

        public Infraccion NuevaInfraccion(string identificador, string descripción, int puntos)
        {
           return  _dgtManager.NuevaInfraccion(identificador, descripción, puntos);
        }

        public bool RegistrarInfraccion(Infraccion i,String mat)
        {
           
            if(!_dgtManager.ExisteMatricula(mat))
            {
                throw new Exception("Matricula inexistente"); 
            }
            Vehiculo veh = _dgtManager.Vehiculos.Where(v => v.Matricula == mat).First();
            return _dgtManager.RegistrarInfraccion(i, veh);
        }


    }

}
