using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.UI.WPF.APIClient;
using DGT.DTOs;
using System.Windows;
using System.Collections.ObjectModel;
using DGT.UI.WPF.Exceptions;

namespace DGT.UI.WPF.ViewModels
{
    public class ConductoresViewModel:ViewModelBase
    {
        DGTApiClient _webApiClient;

        public ConductoresViewModel()
        {
            _webApiClient = new DGTApiClient();
            GetConductores();
        }

        #region bindings

        public ObservableCollection<ConductorDTO> ListaConductores { get; set; }

        private ConductorDTO _conductorSelected;
        public ConductorDTO ConductorSelected
        {
            get { return _conductorSelected; }
            set
            {
                _conductorSelected = value;
                
                RaisePropertyChanged(() => ConductorSelected);
            }
        }



        #endregion

        internal async void Nuevo()
        {
            try
            {
                ConductorDTO c = await _webApiClient.CreateConductor(ConductorSelected);
                //Si el conductor devuelto !=null es porque se creo y lo añadimos a la collection observable
                if (c != null)
                    ListaConductores.Add(c);
                else
                    MessageBox.Show("Conductor existente");
            }
            catch (WebApiException e)
            {
                MessageBox.Show(e.Message);
            }
           
        }

        private void GetConductores()
        {
            var conductores = _webApiClient.GetConductores();
            ListaConductores = new ObservableCollection<ConductorDTO>(conductores.ToList());
        }
    }
}
