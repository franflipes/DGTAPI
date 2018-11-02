using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.Manager;
using DGT.Entities;

namespace DGT.Services
{
    public interface IConductoresService
    {
        List<Conductor> GetConductores();
        List<Conductor> GetTopConductores(int n);
        Conductor GetConductores(Func<Conductor, bool> where);
        Conductor CreateConductor(String dni,String nombre,String apellidos);
    }

    public class ConductoresService:IConductoresService
    {

        private readonly DGTManager _dgtManager;

        #region ctr
        public ConductoresService(DGTManager dgtManager)
        {
            _dgtManager = dgtManager;
        }

        public List<Conductor> getConductores()
        {
            return _dgtManager.Conductores;
        }
        #endregion

        #region Implementacion Interfaz
        public Conductor GetConductores(Func<Conductor, bool> where)
        {
            return _dgtManager.Conductores.Where(where).First() ;
        }

        public List<Conductor> GetConductores()
        {
            return _dgtManager.Conductores;
        }

        
        public Conductor CreateConductor(string dni, string nombre, string apellidos)
        {
            return _dgtManager.CreateConductor(dni, nombre, apellidos);
        }

        public List<Conductor> GetTopConductores(int n)
        {
            return _dgtManager.Conductores.Take(n).ToList();
        }
        #endregion
    }
}
